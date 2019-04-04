using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Client
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Bot _bot;

        public MainWindowViewModel(Bot bot)
        {
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }
    }
}
