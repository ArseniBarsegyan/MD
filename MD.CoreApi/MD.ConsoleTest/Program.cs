using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using MD.Helpers;
using Newtonsoft.Json.Linq;

namespace MD.ConsoleTest
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //Discover the identity server
            var identityServer = await DiscoveryClient.GetAsync(ConstantsHelper.IdentityServerUrl);

            if (identityServer.IsError)
            {
                Console.WriteLine(identityServer.Error);
                return;
            }

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "MDAngular", "MDSecretKey");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("Ars", "LukE4321LukE#", "MD.CoreApi");

            //Call the API
            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync(ConstantsHelper.ApiUrl);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            Console.ReadKey();
        }
    }
}
