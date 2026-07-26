using MusicApp.Views;

namespace MusicApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WelcomeScreen),typeof(WelcomeScreen));
            Routing.RegisterRoute(nameof(PhoneNumberPage),typeof(PhoneNumberPage));
            Routing.RegisterRoute(nameof(OtpPage),typeof(OtpPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(LibraryPage), typeof(LibraryPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        }
    }
}
