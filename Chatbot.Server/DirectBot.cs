using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chatbot.Server
{
    public class DirectBot : IBot
    {
        public Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
