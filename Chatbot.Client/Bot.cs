using AdaptiveCards;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatbot.Client
{
    public class Bot
    {
        private readonly DirectLineSettings _directLineSettings = new DirectLineSettings();
        private readonly DirectLineClient _directLineClient;
        private CancellationTokenSource _cancellationTokenSource;

        private Conversation _conversation;

        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public Bot(IConfiguration configuration)
        {
            configuration.Bind("DirectLine", _directLineSettings);
            _directLineClient = new DirectLineClient(_directLineSettings.Key);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        public async Task SendActivityAsync(string text)
        {
            await CreateConversationIfNotExistAsync();
            await _directLineClient.Conversations.PostActivityAsync(_conversation.ConversationId, new Activity
            {
                From = new ChannelAccount("User"),
                Text = text,
                Type = ActivityTypes.Message,
            });

            Messages.Add(new Message
            {
                MessageFrom = MessageFrom.User,
                Text = text,
            });
        }

        private async Task CreateConversationIfNotExistAsync()
        {
            if (_conversation != null)
            {
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _conversation = await _directLineClient.Conversations.StartConversationAsync(_cancellationTokenSource.Token);
            var ignore = ReadBotMessageAsync(_cancellationTokenSource.Token);
        }

        private async Task ReadBotMessageAsync(CancellationToken cancellationToken)
        {
            string watermark = null;
            while(!cancellationToken.IsCancellationRequested)
            {
                var activitySet = await _directLineClient.Conversations.GetActivitiesAsync(_conversation.ConversationId, watermark, cancellationToken);
                watermark = activitySet.Watermark;
                var botMessages = activitySet.Activities.Where(x => x.From.Id == _directLineSettings.BotId);
                foreach (var message in botMessages)
                {
                    System.Diagnostics.Debug.WriteLine($"{message.Text} received.");
                    Messages.Add(new Message
                    {
                        MessageFrom = MessageFrom.Bot,
                        Text = message.Text,
                        Attachments = ConvertAttachmentsToAdaptiveCard(message.Attachments),
                    });
                }

                await Task.Delay(2000);
            }
        }

        private IEnumerable<AdaptiveCard> ConvertAttachmentsToAdaptiveCard(IEnumerable<Attachment> attachments)
        {
            AdaptiveCard parseHeroCard(string json)
            {
                var heroCard = JsonConvert.DeserializeObject<HeroCard>(json);
                return new AdaptiveCard("1.0")
                {
                    Body = new List<AdaptiveElement>
                    {
                        new AdaptiveTextBlock(heroCard.Title)
                        {
                            Size = AdaptiveTextSize.Medium,
                            Weight = AdaptiveTextWeight.Bolder,
                        },
                        new AdaptiveTextBlock(heroCard.Text),
                    },
                };
            }

            return attachments?.Select(x => x.ContentType switch
                {
                "application/vnd.microsoft.card.hero" => parseHeroCard(x.Content.ToString()),
                "image/png" => new AdaptiveCard("1.0")
                {
                    Body = new List<AdaptiveElement>
                    {
                        new AdaptiveImage(x.ContentUrl),
                    }
                },
                _ => null,
                })
                ?.Where(x => x != null);
        }
    }
}
