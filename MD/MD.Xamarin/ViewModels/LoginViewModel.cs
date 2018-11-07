using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.Authentication;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using MD.Xamarin.Pages;
using MD.Xamarin.Rest;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService = DependencyService.Get<IAlertService>();
        private readonly ILoadingService _loadingService = DependencyService.Get<ILoadingService>();
        private readonly CultureInfo _cultureInfo = CrossMultilingual.Current.CurrentCultureInfo;
        private readonly IIdentityServerClient _identityServerClient = new IdentityServerClient();

        public LoginViewModel()
        {
            LoginCommand = new Command(LoginCommandExecute);
            OAuthLoginCommand = new Command(async() => await OAuthLoginCommandExecute());
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        // Set this field to true to hide error message at LoginPage.
        // When user will press "Login" button and get error change this property
        // will show error at LoginPage.
        public bool IsValid { get; set; } = true;

        public ICommand LoginCommand { get; set; }
        public ICommand OAuthLoginCommand { get; set; }

        private async void LoginCommandExecute()
        {
            _loadingService.ShowLoading();

            // Input fields validation
            if (!AreFieldsValid())
            {
                return;
            }

            string authResult = AuthenticationManager.Authenticate(UserName, Password);
            if (authResult == ConstantsHelper.UserNotExistsMessage)
            {
                var userNotExistsMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.UserNotExistsMessage, _cultureInfo);
                _alertService.ShowOkAlert(userNotExistsMessageLocalized, ConstantsHelper.Ok);
                _loadingService.HideLoading();
                return;
            }
            if (authResult == ConstantsHelper.IncorrectPassword)
            {
                var incorrectPasswordMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.IncorrectPassword, _cultureInfo);
                _alertService.ShowOkAlert(incorrectPasswordMessageLocalized, ConstantsHelper.Ok);
                _loadingService.HideLoading();
                return;
            }
            _loadingService.HideLoading();
            Settings.CurrentUserId = authResult;
            Application.Current.MainPage = new NavigationPage(new NotesPage());
        }

        private async Task OAuthLoginCommandExecute()
        {
            _loadingService.ShowLoading();

            if (!AreFieldsValid())
            {
                return;
            }

            var tokenResponse = await _identityServerClient.GetToken(UserName, Password);
            if (tokenResponse.AccessToken != null)
            {
                Settings.UserAccessToken = tokenResponse.AccessToken;
            }
            else
            {
                var oauthLoginErrorMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.OAuthLoginError, _cultureInfo);
                _alertService.ShowOkAlert(oauthLoginErrorMessageLocalized, ConstantsHelper.Ok);
            }
            _loadingService.HideLoading();
        }

        private bool AreFieldsValid()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                var userNameEmptyMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.UsernameEmptyMessage, _cultureInfo);
                _alertService.ShowOkAlert(userNameEmptyMessageLocalized, ConstantsHelper.Ok);
                _loadingService.HideLoading();
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                var passwordEmptyMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.PasswordEmptyMessage, _cultureInfo);
                _alertService.ShowOkAlert(passwordEmptyMessageLocalized, ConstantsHelper.Ok);
                _loadingService.HideLoading();
                return false;
            }
            return true;
        }
    }
}
