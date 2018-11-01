using System.Windows.Input;
using MD.Xamarin.Authentication;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using MD.Xamarin.Pages;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            RegisterCommand = new Command(RegisterCommandExecute);
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand { get; set; }

        private async void RegisterCommandExecute()
        {
            var alertService = DependencyService.Get<IAlertService>();
            var loadingService = DependencyService.Get<ILoadingService>();
            loadingService.ShowLoading();
            var ci = CrossMultilingual.Current.CurrentCultureInfo;

            // Input fields validation

            if (string.IsNullOrWhiteSpace(UserName))
            {
                var userNameEmptyMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.UsernameEmptyMessage, ci);
                alertService.ShowOkAlert(userNameEmptyMessageLocalized, ConstantsHelper.Ok);
                loadingService.HideLoading();
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                var passwordEmptyMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.PasswordEmptyMessage, ci);
                alertService.ShowOkAlert(passwordEmptyMessageLocalized, ConstantsHelper.Ok);
                loadingService.HideLoading();
                return;
            }
            if (Password != ConfirmPassword)
            {
                var passwordsDoesNotMatchMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.PasswordsDoesNotMatchMessage, ci);
                alertService.ShowOkAlert(passwordsDoesNotMatchMessageLocalized, ConstantsHelper.Ok);
                loadingService.HideLoading();
                return;
            }

            bool registrationResult = await AuthenticationManager.Register(UserName, Password);
            loadingService.HideLoading();
            if (!registrationResult)
            {
                alertService.ShowOkAlert(ConstantsHelper.UserAlreadyExistsMessage, ConstantsHelper.Ok);
                return;
            }
            alertService.ShowOkAlert(ConstantsHelper.RegisterSuccessful, ConstantsHelper.Ok);
            Application.Current.MainPage = new LoginPage();
        }
    }
}