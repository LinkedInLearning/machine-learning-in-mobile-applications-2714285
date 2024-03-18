using Microsoft.Extensions.Logging;
using MLSample.ViewModels;
using MLSample.Views;

namespace MLSample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<VisualRecognitionViewModel>();
		builder.Services.AddTransient<HousingPredictionViewModel>();
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<VisualRecognitionViewModel>();
		builder.Services.AddTransient<HousingPredictionViewModel>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<HousingPrediction>();
		builder.Services.AddTransient<VisualRecognition>();
		

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
