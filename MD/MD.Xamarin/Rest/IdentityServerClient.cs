using System.Net;
using System.Threading.Tasks;
using IdentityModel.Client;
using MD.Xamarin.Helpers;

namespace MD.Xamarin.Rest
{
    public class IdentityServerClient : IIdentityServerClient
    {
        private DiscoveryResponse _identityServerResponse;

        private async Task<bool> InitializeIdentityServer()
        {
            _identityServerResponse = await DiscoveryClient.GetAsync(ConstantsHelper.IdentityServerUrl);
            return !_identityServerResponse.IsError;
        }

        public async Task<TokenResponse> GetToken(string username, string password)
        {
            var isIdentityServerAvailable = await InitializeIdentityServer();
            if (!isIdentityServerAvailable)
            {
                return new TokenResponse(HttpStatusCode.NotFound, "Identity server access", "Can't discover identity server");
            }
            var tokenClient = new TokenClient(_identityServerResponse.TokenEndpoint, ConstantsHelper.XamarinClientId, "MDXamarinClientSecretKey");
            return await tokenClient.RequestResourceOwnerPasswordAsync(username, password, ConstantsHelper.ApiName);
        }
    }
}