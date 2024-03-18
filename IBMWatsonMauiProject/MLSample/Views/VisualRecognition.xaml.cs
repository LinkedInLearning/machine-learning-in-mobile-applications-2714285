using System;
using System.Collections.Generic;
using MLSample.ViewModels;
using Microsoft.Maui;

namespace MLSample.Views
{
    public partial class VisualRecognition : ContentPage
    {
        public VisualRecognition(VisualRecognitionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Title = "Visual Recognition";
            lstImages.ItemTapped += ItemTapped;
        }

        private void ItemTapped(object? sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as VisualRecognitionViewModel;
            if (vm != null)
            {
                vm.EvaluateImage.Execute(((Models.ImageItem)e.Item).ImagePath);
            }
        }
    }
}
