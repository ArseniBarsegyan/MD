using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MD.Xamarin.EF.Models;
using MD.Xamarin.Helpers;

namespace MD.Xamarin.Authentication
{
    /// <summary>
    /// Class for test purposes. Implements authentication logic of the app for SQLite database.
    /// </summary>
    public static class AuthenticationManager
    {
        /// <summary>
        /// Attempt to login user. If user doesn't exist return false. If user's credentials are wrong return false.
        /// </summary>
        /// <param name="userName">user name.</param>
        /// <param name="password">password.</param>
        /// <returns></returns>
        public static string Authenticate(string userName, string password)
        {
            var user = App.UserManager.GetAll().FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return ConstantsHelper.UserNotExistsMessage;
            }
            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var passwordHash = SHA256.Create().ComputeHash(passwordBytes);
            var passwordHashAsStringValue = Convert.ToBase64String(passwordHash);

            if (userName == user.UserName && passwordHashAsStringValue.SequenceEqual(user.PasswordHash))
            {
                return user.Id;
            }
            return ConstantsHelper.IncorrectPassword;
        }

        /// <summary>
        /// Attempt to register user in SQLite database. Return false if user already exists.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public static async Task<bool> Register(string userName, string password)
        {
            var user = App.UserManager.GetAll().FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                return false;
            }
            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var passwordHash = SHA256.Create().ComputeHash(passwordBytes);
            var passwordHashAsStringValue = Convert.ToBase64String(passwordHash);

            var userModel = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = userName,
                PasswordHash = passwordHashAsStringValue
            };
            await App.UserManager.CreateAsync(userModel);
            await App.UserManager.SaveAsync();
            return true;
        }
    }
}