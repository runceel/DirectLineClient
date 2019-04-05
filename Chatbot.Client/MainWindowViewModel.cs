using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;

namespace Chatbot.Client
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Bot _bot;

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                if (Set(ref _inputText, value))
                {
                    SendMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isSendingMessage;

        private bool IsSendingMessage
        {
            get => _isSendingMessage;
            set
            {
                if (Set(ref _isSendingMessage, value))
                {
                    SendMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private RelayCommand _sendMessageCommand;
        public RelayCommand SendMessageCommand => _sendMessageCommand ??
            (_sendMessageCommand = new RelayCommand(SendMessageExecute, SendMessageCanExecute));

        public ReadOnlyObservableCollection<Message> Messages { get; }

        private async void SendMessageExecute()
        {
            IsSendingMessage = true;
            try
            {
                await _bot.SendActivityAsync(InputText);
                InputText = "";
            }
            finally
            {
                IsSendingMessage = false;
            }
        }

        private bool SendMessageCanExecute() => !string.IsNullOrWhiteSpace(InputText) && !IsSendingMessage;

        public void Closed()
        {
            _bot.Stop();
        }

        public MainWindowViewModel(Bot bot)
        {
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
            Messages = new ReadOnlyObservableCollection<Message>(_bot.Messages);
            BindingOperations.EnableCollectionSynchronization(Messages, new object());
        }
    }
}
