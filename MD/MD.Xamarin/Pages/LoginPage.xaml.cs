using System;
using System.Diagnostics;
using System.Net.Http;
using IdentityModel.Client;
using MD.Xamarin.Helpers;
using MD.Xamarin.Rest;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD.Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BackgroundImage.Source = ImageSource.FromResource(ConstantsHelper.LoginPageBackgroundImage);
        }

        private void TogglePasswordVisibilityButton_OnTapped(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        }

        private void UserNameEntry_OnCompleted(object sender, EventArgs e)
        {
            PasswordEntry.Focus();
        }

        private void PasswordEntry_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.LoginCommand.Execute(null);
        }

        private void RegisterLink_OnTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage();
        }

        private async void OAuthButton_OnClicked(object sender, EventArgs e)
        {
            ViewModel.OAuthLoginCommand.Execute(null);
        }
    }
}