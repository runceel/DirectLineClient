using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Chatbot.Client.Core
{
    public static class ViewModelLocator
    {
        public static void Initialize(string basePath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.debug.json", optional: true)
                .Build();

            var bot = new Bot(configuration, new SynchronizationContextScheduler(SynchronizationContext.Current));
            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register(() => bot);

        }
        public static MainWindowViewModel MainWindowViewModel => SimpleIoc.Default.GetInstance<MainWindowViewModel>();
    }
}
