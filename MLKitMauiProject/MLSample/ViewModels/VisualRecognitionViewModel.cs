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
using Microsoft.Maui.Graphics.Platform;

#if __ANDROID__
using Xamarin.Google.MLKit.Vision.Label;
using Xamarin.Google.MLKit.Vision.Label.Defaults;
using Xamarin.Google.MLKit.Vision.Common;
#endif

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
            try
            {
                if (image is string && !string.IsNullOrEmpty(image as string))
                {
                    var fileName = (string)image;
                    var result = await GetTreeTypeAsync(fileName);
                    ImageType = result;
                }
                else
                {
                    ImageType = $"No idea what {image.ToString()} is";
                }
            }
            finally
            {
                IsBusy = false;
            }

        }

        private Task<string> GetTreeTypeAsync(string fileName)
        {
            var tcs = new TaskCompletionSource<string>();

#if __ANDROID__
            using (var labeler = ImageLabeling.GetClient(ImageLabelerOptions.DefaultOptions))
            {
                using (var imageStream = ImageLoader.LoadImageStreamAsync(fileName).Result)
                {
                    byte[] imageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        imageStream.CopyTo(memoryStream);
                        imageBytes =  memoryStream.ToArray();
                    }
                    var image = Xamarin.Google.MLKit.Vision.Common.InputImage.FromByteArray(imageBytes, 480, 360, 0, InputImage.ImageFormatNv21);

                    var labels = labeler.Process(image).AddOnSuccessListener(new ImageOnSuccessListener(tcs)).AddOnFailureListener(new ImageOnFailureListener(tcs));
                }   
            }
#else
            tcs.SetResult("Not available on this platform"); 
#endif

            return tcs.Task;
        }

        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

#if __ANDROID__
    internal class ImageOnSuccessListener : Java.Lang.Object, Android.Gms.Tasks.IOnSuccessListener
    {
        private TaskCompletionSource<string> _tcs;
        internal ImageOnSuccessListener(TaskCompletionSource<string> tcs)
        {
            _tcs = tcs;
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            var labelResult = (Android.Runtime.JavaList)result;
            string returnValue = string.Empty;
            foreach (ImageLabel suggestion in labelResult.ToArray())
            {
                returnValue += suggestion.Text + Environment.NewLine;
            }
            _tcs.SetResult(returnValue);
        }
    }

    internal class ImageOnFailureListener : Java.Lang.Object, Android.Gms.Tasks.IOnFailureListener
    {
        private TaskCompletionSource<string> _tcs;
        internal ImageOnFailureListener(TaskCompletionSource<string> tcs)
        {
            _tcs = tcs;
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            _tcs.SetResult($"Error getting result: {e.Message}");
        }
    }
#endif
}