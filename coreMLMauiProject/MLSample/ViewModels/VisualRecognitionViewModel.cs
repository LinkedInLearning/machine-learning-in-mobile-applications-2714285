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
#if __IOS__
using CoreVideo;
using UIKit;
using CoreGraphics;
using CoreML;
using Foundation;
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

        private async Task<string> GetTreeTypeAsync(string fileName)
        {
            string returnValue = "well I'm not sure";

            return returnValue;
        }

#if __IOS__
        private UIImage ScaleImage(UIImage image, int width, int height)
        {
            UIImage returnValue;
            UIKit.UIGraphics.BeginImageContextWithOptions(new CGSize(width, height), false, 0.0f);

            image.Draw(new CGRect(new CGPoint(), size: new CGSize(width, height)));
            returnValue =  UIKit.UIGraphics.GetImageFromCurrentImageContext();
            UIKit.UIGraphics.EndImageContext();
            return returnValue;
        }


        private CVPixelBuffer ConvertImageToBuffer(UIImage image)
        {
            var attrs = new CVPixelBufferAttributes();
			attrs.CGImageCompatibility = true;
			attrs.CGBitmapContextCompatibility = true;

			var cgImg = image.CGImage;

			var pb = new CVPixelBuffer(cgImg.Width, cgImg.Height, CVPixelFormatType.CV32ARGB, attrs);
			pb.Lock(CVPixelBufferLock.None);
			var pData = pb.BaseAddress;
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var ctxt = new CGBitmapContext(pData, cgImg.Width, cgImg.Height, 8, pb.BytesPerRow, colorSpace, CGImageAlphaInfo.NoneSkipFirst);
			ctxt.TranslateCTM(0, cgImg.Height);
			ctxt.ScaleCTM(1.0f, -1.0f);
			UIGraphics.PushContext(ctxt);
			image.Draw(new CGRect(0, 0, cgImg.Width, cgImg.Height));
			UIGraphics.PopContext();
			pb.Unlock(CVPixelBufferLock.None);

            return pb;
        }
#endif

        private void PropertyIsChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}