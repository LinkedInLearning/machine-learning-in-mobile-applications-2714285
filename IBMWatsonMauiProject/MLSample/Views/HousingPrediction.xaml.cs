using MLSample.ViewModels;

namespace MLSample.Views
{
    public partial class HousingPrediction : ContentPage
    {
        public HousingPrediction(HousingPredictionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Title = "House Vote Prediction";
        }
    }
}