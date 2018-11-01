using System.Linq;
using System.Windows.Controls;
using MD.WPF.Commands;
using MD.WPF.Helpers;
using MD.WPF.Models;
using MD.WPF.Pages;
using MD.WPF.Properties;

namespace MD.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private RestClientCommand _loginCommand;
        private string _email;
        private string _errorMessage;

        public LoginViewModel()
        {
            if (!string.IsNullOrEmpty(Settings.Default.UserName))
            {
                Email = Settings.Default.UserName;
            }            
        }

        public string Email
        {
            get => _email;
            set => SetValue(ref _email, value, nameof(Email));
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetValue(ref _errorMessage, value, nameof(ErrorMessage));
        }

        public RestClientCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RestClientCommand(async (parameter) =>
                       {
                           var values = (object[])parameter;
                           var passwordBox = values.ElementAt(0) as PasswordBox;
                           var password = passwordBox?.Password;

                           var loginWindow = values.ElementAt(1) as LoginWindow;

                           if (string.IsNullOrWhiteSpace(Email))
                           {
                               ErrorMessage = ConstantsHelper.EmailEmptyError;
                               return;
                           }

                           if (string.IsNullOrWhiteSpace(password))
                           {
                               ErrorMessage = ConstantsHelper.PasswordEmptyError;
                               return;
                           }

                           var loginModel = new LoginModel
                           {
                               Email = Email,
                               Password = password
                           };
                           await ServiceClient.Login(loginModel);

                           if (string.IsNullOrEmpty(Settings.Default.Token))
                           {
                               ErrorMessage = ConstantsHelper.NotRegisteredOrPasswordIncorrectError;
                           }
                           else
                           {
                               ErrorMessage = string.Empty;
                               Settings.Default.UserName = Email;
                               Settings.Default.IsUserLogin = true;
                               
                               var mainWindow = new MainWindow();
                               mainWindow.Show();
                               loginWindow?.Close();
                           }
                       }));
            }
        }
    }
}