using Microsoft.Extensions.Logging;
using Music2._0.ViewModels;
using MusicApp.ViewModels;
using MusicApp.Views;

namespace MusicApp
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<WelcomeScreen>();
            builder.Services.AddTransient<WelcomeScreenViewModel>();
            builder.Services.AddTransient<PhoneNumberPage>();
            builder.Services.AddTransient<PhoneNumberViewModel>();
            builder.Services.AddTransient<OtpPage>();
            builder.Services.AddTransient<OtpPageViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<PlayerPage>();
            builder.Services.AddTransient<PlayerPageViewModel>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<SearchPageViewModel>();
            builder.Services.AddTransient<LibraryPage>();
            builder.Services.AddTransient<LibraryPageViewModel>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfilePageViewModel>();

            return builder.Build();
        }
    }
}
