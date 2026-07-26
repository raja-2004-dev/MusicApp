using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
    public  partial class WelcomeScreenViewModel:ObservableObject
    {
        public AsyncRelayCommand GetStartedCommand { get; set; }

        public WelcomeScreenViewModel()
        {
            GetStartedCommand = new AsyncRelayCommand(GetStartedCommandHandler);
        }
        
        async Task GetStartedCommandHandler()
        {
            await Shell.Current.GoToAsync(nameof(PhoneNumberPage));
        }
    }
}
