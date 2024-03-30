using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MLSample.Constants;
using MLSample.Models;
using MLSample.Views;
using Newtonsoft.Json;
using Microsoft.Maui;

namespace MLSample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
#if __ANDROID__
        List<Xamarin.Google.MLKit.NL.SmartReply.TextMessage> _conversation;
#endif

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
#if __ANDROID__
            _conversation = new List<Xamarin.Google.MLKit.NL.SmartReply.TextMessage>();
#endif
        }

        private ObservableCollection<ConversationItem> _AppConversation = new ObservableCollection<ConversationItem>();
        public ObservableCollection<ConversationItem> AppConversation
        {
            get
            {
                return _AppConversation;
            }
            private set
            {
                if (_AppConversation != value)
                {
                    _AppConversation = value;
                    PropertyIsChanged(nameof(AppConversation));
                }
            }
        }

        private Command _sendText;
        public Command SendText
        {
            get
            {
                if (_sendText == null)
                {
                    _sendText = new Command(async () => await SendTextAsync());
                }
                return _sendText;
            }
        }

        private Command _imageRecognition;
        public Command ImageRecognition
        {
            get
            {
                if (_imageRecognition == null)
                {
                    _imageRecognition = new Command(async () => await ImageRecognitionAsync());
                }
                return _imageRecognition;
            }
        }

        private string _UserText;
        public string UserText
        {
            get
            {
                return _UserText;
            }
            set
            {
                if (_UserText != value)
                {
                    _UserText = value;
                    PropertyIsChanged(nameof(UserText));
                }
            }
        }

        private async Task SendTextAsync()
        {
            if (string.IsNullOrEmpty(UserText)) return;
            try
            {
                var requestText = UserText;
                UserText = string.Empty;

                AddNewMessage(requestText, true);

                AddNewMessage(await GetReply(requestText), false);

            }
            catch (Exception)
            {
                AddNewMessage("I'm sorry, I encountered an error and I don't know why. I may no longer be stable. Just what do you think you are doing Dave?", false);
            }
        }

        private async Task ImageRecognitionAsync()
        {
            await Shell.Current.GoToAsync("VisualRecognition");
        }

        private void AddNewMessage(string message, bool client)
        {
            AppConversation.Add(new ConversationItem
            {
                Message = message.Replace(Environment.NewLine, " "),
                ClientMessage = client
            });
#if __ANDROID__
            if (client)
            {
                _conversation.Add(Xamarin.Google.MLKit.NL.SmartReply.TextMessage.CreateForRemoteUser(message, DateTime.Now.Ticks, "RealUser"));
            }
            else
            {
                _conversation.Add(Xamarin.Google.MLKit.NL.SmartReply.TextMessage.CreateForLocalUser(message.Split(Environment.NewLine)[0], DateTime.Now.Ticks));                 
            }
#endif
        }

        private Task<string> GetReply(string enteredText)
        {
            var returnValue = string.Empty;

            var tcs = new TaskCompletionSource<string>();

            return tcs.Task;
        }

        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


