using Microsoft.Extensions.DependencyInjection;
using MusicApp.Views;

namespace MusicApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}