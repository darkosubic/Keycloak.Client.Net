namespace Keycloak.Client.Net.Constants
{
    internal static class TokenGrantType
    {
        public const string AuthorizationCode = "authorization_code";
        public const string ClientCredentials = "client_credentials";
        public const string Password = "password";
        public const string RefreshToken = "refresh_token";
        public const string DeviceAuthorization = "urn:ietf:params:oauth:grant-type:device_code";
    }
}
