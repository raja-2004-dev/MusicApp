using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
    public class PhoneNumberViewModel: ObservableObject
    {
       string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set=> SetProperty(ref _phoneNumber, value);
        }
        public AsyncRelayCommand ContinueCommand { get; set; }

        public PhoneNumberViewModel()
        {
            ContinueCommand = new AsyncRelayCommand(ContinueCommandHandler);
        }

        async Task ContinueCommandHandler()
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await Shell.Current.DisplayAlert("Error", "Please Enter PhoneNumber", "Cancel");
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(OtpPage));

            }
        }
    }
}
