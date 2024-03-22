using MLSample.Views;

namespace MLSample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		Routing.RegisterRoute("VisualRecognition", typeof(VisualRecognition));
		Routing.RegisterRoute("HousingPrediction", typeof(HousingPrediction));
	}
}
