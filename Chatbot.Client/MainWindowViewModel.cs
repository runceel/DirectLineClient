using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Client
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IConfiguration _configuration;

        public MainWindowViewModel(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
    }
}
