using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Chatbot.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Assembly.GetExecutingAssembly().Location)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.debug.json", optional: true)
                .Build();

            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register<MainWindowViewModel>();
        }
    }
}
