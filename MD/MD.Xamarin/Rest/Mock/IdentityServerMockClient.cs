using System.Threading.Tasks;
using IdentityModel.Client;

namespace MD.Xamarin.Rest.Mock
{
    /// <inheritdoc />
    /// <summary>
    /// Fake implementation of <see cref="T:MD.Xamarin.Rest.IIdentityServerClient" />
    /// </summary>
    public class IdentityServerMockClient : IIdentityServerClient
    {
        /// <summary>
        /// Return fake token.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetToken(string username, string password)
        {
            return await Task.FromResult(new TokenResponse("success"));
        }
    }
}