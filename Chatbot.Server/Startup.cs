using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Server
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var botConfig = BotConfiguration.Load("DirectBot.bot");
            services.AddSingleton(botConfig);

            services.AddBot<DirectBot>(options =>
            {
                var endpoint = botConfig.Services
                    .Where(x => x.Type == "endpoint")
                    .Where(x => x.Name == (_env.IsProduction() ? "production" : "development"))
                    .OfType<EndpointService>()
                    .FirstOrDefault() ?? throw new InvalidOperationException("The endpoint not found.");
                options.CredentialProvider = new SimpleCredentialProvider(endpoint.AppId, endpoint.AppPassword);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
