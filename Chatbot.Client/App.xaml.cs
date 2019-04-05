using Chatbot.Client.Core;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
            ViewModelLocator.Initialize(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
