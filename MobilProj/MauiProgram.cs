using Microsoft.Extensions.Logging;
using MobilProj.Services;
using MobilProj.View;
using MobilProj.ViewModel;
using Microcharts.Maui;

namespace MobilProj
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<RaceService>();

            builder.Services.AddTransient<DriverStandingsViewModel>();
            builder.Services.AddTransient<DriverStandingsPage>();

            builder.Services.AddTransient<ConstructorStandingsViewModel>();
            builder.Services.AddTransient<ConstructorStandingsPage>();

            builder.Services.AddTransient<RacesViewModel>();
            builder.Services.AddTransient<RacesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
