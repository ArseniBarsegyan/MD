using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using MD.Helpers;

namespace IdentityServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "MDAngular",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("MDSecretKey".Sha256())
                    },
                    AllowedScopes = { "MD.CoreApi" }
                },

                // Angular client
                new Client {
                    ClientId = ConstantsHelper.AngularClientId,
                    ClientName = ConstantsHelper.AngularClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = ConstantsHelper.AngularClientAllowedScopes,
                    RedirectUris = ConstantsHelper.AngularClientRedirectUris,
                    PostLogoutRedirectUris = ConstantsHelper.AngularClientPostLogoutRedirectUris,
                    AllowedCorsOrigins = ConstantsHelper.AngularClientAllowedCorsOrigins,
                    AllowAccessTokensViaBrowser = true
                },

                // Xamarin client
                new Client
                {
                    ClientId = ConstantsHelper.XamarinClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("MDXamarinClientSecretKey".Sha256())
                    },
                    AllowedScopes = { ConstantsHelper.ApiName }
                },

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = ConstantsHelper.MvcClientId,
                    ClientName = ConstantsHelper.MvcClientName,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("MDSecretKey".Sha256())
                    },

                    RedirectUris = ConstantsHelper.MvcClientRedirectUris,
                    PostLogoutRedirectUris = ConstantsHelper.MvcClientPostLogoutRedirectUris,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        ConstantsHelper.ApiName
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}