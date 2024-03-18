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
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.NaturalLanguageUnderstanding.v1;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;

namespace MLSample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {

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

                var userIntent = await GetIntentFromText(requestText);

                switch (userIntent)
                {
                    case UserIntent.Unknown:
                        AddNewMessage("Say what?", false);
                        break;
                    case UserIntent.Greeting:
                        AddNewMessage("Hello and welcome!", false);
                        break;
                    case UserIntent.Help:
                        AddNewMessage("You can ask me to either evaluate the contents of an image or predict the budget vote on a House of Representatives member", false);
                        break;
                    case UserIntent.VisualRecognition:
                        AddNewMessage("OK, let's do some visual Recognition", false);
                        await Task.Delay(2000);
                        await Shell.Current.GoToAsync("VisualRecognition");
                        break;
                    case UserIntent.PricePrediction:
                        AddNewMessage("Great, let's try and predict some 1970 Boston home prices", false);
                        await Task.Delay(2000);
                        await Shell.Current.GoToAsync("HousingPrediction");
                        break;
                }
            }
            catch (WebException)
            {
                AddNewMessage("Ah yeah sorry, I can't connect to my brain.", false);
            }
            catch (Exception)
            {
                AddNewMessage("I'm sorry, I encountered an error and I don't know why. I may no longer be stable. Just what do you think you are doing Dave?", false);
            }
        }

        private void AddNewMessage(string message, bool client)
        {
            AppConversation.Add(new ConversationItem
            {
                Message = message,
                ClientMessage = client
            });
        }

        private async Task<UserIntent> GetIntentFromText(string enteredText)
        {
            UserIntent returnIntent = UserIntent.Unknown;

            var topScoringIntent = await GetTopScoringIntent(enteredText);
            if (topScoringIntent.Score >= .55f)
            {
                returnIntent = topScoringIntent.Intent;
            }

            return returnIntent;
        }

        private Task<TopScoringIntent> GetTopScoringIntent(string enteredText)
        {

            var returnValue = new TopScoringIntent { Score = 1, Intent = UserIntent.Unknown };

            var tcs = new TaskCompletionSource<TopScoringIntent>();

            tcs.SetResult(returnValue);

            return tcs.Task;
        }

        private UserIntent GetIntentFromClass(string className)
        {
            var returnValue = UserIntent.Unknown;
            switch (className.ToLower().Trim())
            {
                case "greeting":
                    returnValue = UserIntent.Greeting;
                    break;
                case "help":
                    returnValue = UserIntent.Help;
                    break;
                case "visual_recognition":
                    returnValue = UserIntent.VisualRecognition;
                    break;
                case "price_prediction":
                    returnValue = UserIntent.PricePrediction;
                    break;
                case "none":
                    returnValue = UserIntent.Unknown;
                    break;
            }
            return returnValue;
        }


        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
