using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        //Defining the InMemory Clients
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "Client1",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RequireConsent = false,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:51053/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:51053/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true //It should be true to send Claims with token
                }
            };
            return clients;
        }

        //Defining the InMemory API's
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var apiResources = new List<ApiResource>
            {
                new ApiResource("api1", "First API")
            };
            return apiResources;
        }

        //Defining the InMemory User's
        public static List<TestUser> GetUsers()
        {
            List<TestUser> testUsers = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="admin",
                    Password="password",

                    Claims = new List<Claim>
                    {
                        new Claim("role", "admin"),
                    }
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="editor",
                    Password="password",

                    Claims = new List<Claim>
                    {
                        new Claim("role", "editor"),
                    }
                }
            };
            return testUsers;
        }

        //Support for OpenId connectivity scopes
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var resources = new List<IdentityResource>();

            resources.Add(new IdentityResources.OpenId()); // Support for the OpenId
            resources.Add(new IdentityResources.Profile()); // To get the profile information
            resources.Add(new IdentityResource("roles", new[] { "role" })); //To support roles

            return resources;
        }
    }
}