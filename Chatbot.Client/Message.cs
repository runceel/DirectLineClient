using AdaptiveCards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Client
{
    public class Message
    {
        public MessageFrom MessageFrom { get; set; }
        public string Text { get; set; }
        public IEnumerable<AdaptiveCard> Attachments { get; set; }
    }

    public enum MessageFrom
    {
        User,
        Bot,
    }
}
