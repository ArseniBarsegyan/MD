using System.Collections.Generic;

namespace MD.Helpers
{
    public static class ConstantsHelper
    {
        public const string ContextShemaName = "dbo";
        public const string DefaultConnection = "DefaultConnection";
        public const string EmailIsEmpty = "Email is empty";
        public const string PasswordIsEmpty = "Password is empty";
        public const string PasswordErrorMessage = "PASSWORD_MIN_LENGTH";
        public const string PasswordIncorrect = "Password incorrect";

        public const string ApiName = "MD.CoreApi";
        public const string AuthenticationType = "Bearer";
        public const string CorsPolicy = "CorsPolicy";
        public const string IdentityServerUrl = "https://identityserver20181030042351.azurewebsites.net/";
        public const string ReleaseVersionConnection = "AzureDatabaseConnection";

        // Angular client constants
        public const string AngularClientId = "Angular_client";
        public const string AngularClientName = "Angular 4 Client";
        public static List<string> AngularClientAllowedScopes = new List<string> { "openid", "profile", ApiName, "custom.profile" };
        public static List<string> AngularClientRedirectUris = new List<string> {"http://localhost:4200/auth-callback"};
        public static List<string> AngularClientPostLogoutRedirectUris = new List<string> { "http://localhost:4200/logout-callback" };
        public static List<string> AngularClientAllowedCorsOrigins = new List<string> {"http://localhost:4200"};

        // Xamarin client constants
        public const string XamarinClientId = "XamarinClient";

        //MVC client constants
        public const string MvcClientId = "MDMVC";
        public const string MvcClientName = "MVC Client";
        public static List<string> MvcClientRedirectUris = new List<string> { "http://localhost:51866/signin-oidc" };
        public static List<string> MvcClientPostLogoutRedirectUris = new List<string> { "http://localhost:51866/signout-callback-oidc" };
    }
}