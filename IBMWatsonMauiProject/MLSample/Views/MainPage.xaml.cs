using MLSample.ViewModels;

namespace MLSample.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}

