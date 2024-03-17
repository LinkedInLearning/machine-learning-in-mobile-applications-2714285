using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MLSample.Interfaces;
using MLSample.Models;
using Newtonsoft.Json;
using Microsoft.Maui;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;

namespace MLSample.ViewModels
{
    public class VisualRecognitionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public VisualRecognitionViewModel()
        {
            ImageList = new ObservableCollection<ImageItem>
            {
                new ImageItem { ImagePath = "th_1.jpg" },
                new ImageItem { ImagePath = "th_2.jpg" },
                new ImageItem { ImagePath = "th_3.jpg" },
                new ImageItem { ImagePath = "th_4.jpg" },
                new ImageItem { ImagePath = "th_5.jpg" },
                new ImageItem { ImagePath = "th_6.jpg" },
                new ImageItem { ImagePath = "th_7.jpg" },
                new ImageItem { ImagePath = "th_8.jpg" },
                new ImageItem { ImagePath = "th_9.jpg" },
            };
        }

        private ObservableCollection<ImageItem> _ImageList = new ObservableCollection<ImageItem>();
        public ObservableCollection<ImageItem> ImageList
        {
            get
            {
                return _ImageList;
            }
            private set
            {
                if (_ImageList != value)
                {
                    _ImageList = value;
                    PropertyIsChanged(nameof(ImageList));
                }
            }
        }

        private string _ImageType = string.Empty;
        public string ImageType
        {
            get
            {
                return _ImageType;
            }
            private set
            {
                if (_ImageType != value)
                {
                    _ImageType = value;
                    PropertyIsChanged(nameof(ImageType));
                }
            }
        }

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            private set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    PropertyIsChanged(nameof(IsBusy));
                }
            }
        }

        private Command? _evaluateImage = null;
        public Command EvaluateImage
        {
            get
            {
                if (_evaluateImage == null)
                {
                    _evaluateImage = new Command(async (image) => await EvaluateImageAsync(image));
                }
                return _evaluateImage;
            }
        }

        private async Task EvaluateImageAsync(object image)
        {
            IsBusy = true;
            ImageType = $"Evaluating {image.ToString()}";
            string predictionEndpoint = "{URL}";
            string apiKey = "{APIKey}";
            try
            {
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(apiKey);
                CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(credentials)
                {
                    Endpoint = predictionEndpoint
                };

                using (Stream imageStream = await ImageLoader.LoadImageStreamAsync(image as string))
                {

                        var result = await predictionApi.ClassifyImageAsync(new Guid("{projectId}"), "{deployement name}", imageStream);

                        if (result != null && result.Predictions != null && result.Predictions.Count > 0)
                        {
                            var prediction = result.Predictions.OrderByDescending(p => p.Probability).First();
                            ImageType = $"{prediction.TagName} ({prediction.Probability:P1})";
                        }
                        else
                        {
                            ImageType = $"No idea what {image.ToString()} is";
                        }
                }
            }
            finally
            {
                IsBusy = false;
            }

        }

        private Task<string> GetTreeTypeAsync(BinaryReader binaryReader, long byteLength, string fileName)
        {
            string returnValue = "well I'm not sure";

            var tcs = new TaskCompletionSource<string>();
            tcs.SetResult(returnValue);

            return tcs.Task;
        }

        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}