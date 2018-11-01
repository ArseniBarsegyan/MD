using System.Threading.Tasks;
using IdentityModel.Client;

namespace MD.Xamarin.Rest
{
    /// <summary>
    /// Provide access to Identity Server.
    /// </summary>
    public interface IIdentityServerClient
    {
        /// <summary>
        /// Get access and refresh tokens from Identity server.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="password">password.</param>
        /// <returns></returns>
        Task<TokenResponse> GetToken(string username, string password);
    }
}