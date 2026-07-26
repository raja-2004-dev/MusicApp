using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
        public partial class OtpPageViewModel : ObservableObject
        {
            [ObservableProperty]
            private string otp1 = string.Empty;

            [ObservableProperty]
            private string otp2 = string.Empty;

            [ObservableProperty]
            private string otp3 = string.Empty;

            [ObservableProperty]
            private string otp4 = string.Empty;

            [ObservableProperty]
            private string otp5 = string.Empty;

            [ObservableProperty]
            private string otp6 = string.Empty;
          
            public AsyncRelayCommand VerifyCommand { get; set; }


        public OtpPageViewModel()
        {
            VerifyCommand = new AsyncRelayCommand(VerifyCommandHandler);
        }
        async Task VerifyCommandHandler()
        {
            string otp = $"{Otp1}{Otp2}{Otp3}{Otp4}{Otp5}{Otp6}";

            if (otp.Length != 6)
            {
                await Shell.Current.DisplayAlert(
                    "Verification",
                    "Please enter all 6 digits.",
                    "OK");

                return;
            }

                if (otp == "123456")
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    "Verification",
                    "Invalid verification code.",
                    "OK");
            }
        }

        [RelayCommand]
        private void Clear()
        {
            Otp1 = string.Empty;
            Otp2 = string.Empty;
            Otp3 = string.Empty;
            Otp4 = string.Empty;
            Otp5 = string.Empty;
            Otp6 = string.Empty;
        }
    }


}
