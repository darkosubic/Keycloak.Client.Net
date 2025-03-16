using Keycloak.Client.Net.Constants;

namespace Keycloak.Client.Net.Builders
{
    public class ClientCredentialsBuilder
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _clientId = string.Empty;
        private string _clientSecret = string.Empty;
        private string _grantType = string.Empty;
        private string _code = string.Empty;
        private string _redirectUri = string.Empty;
        private string _deviceCode = string.Empty;

        public static ClientCredentialsBuilder New() => new ClientCredentialsBuilder();

        public ClientCredentialsBuilder Username(string username)
        {
            _username = username;

            return this;
        }

        public ClientCredentialsBuilder Password(string password)
        {
            _password = password;
            return this;
        }

        public ClientCredentialsBuilder ClientId(string clientId)
        {
            _clientId = clientId;

            return this;
        }

        public ClientCredentialsBuilder ClientSecret(string clientSecret)
        {
            _clientSecret = clientSecret;
            return this;
        }

        public ClientCredentialsBuilder Code(string code)
        {
            _code = code;
            return this;
        }

        public ClientCredentialsBuilder RedirectUri(string redirectUri)
        {
            _redirectUri = redirectUri;
            return this;
        }

        public ClientCredentialsBuilder DeviceCode(string deviceCode)
        {
            _deviceCode = deviceCode;
            return this;
        }


        public ClientCredentialsBuilder UseAuthorizationCodeGrantType()
        {
            _grantType = TokenGrantType.AuthorizationCode;
            return this;
        }

        public ClientCredentialsBuilder UseClientCredentialsGrantType()
        {
            _grantType = TokenGrantType.ClientCredentials;
            return this;
        }

        public ClientCredentialsBuilder UsePasswordGrantType()
        {
            _grantType = TokenGrantType.Password;
            return this;
        }

        public ClientCredentialsBuilder UseDeviceAuthorizationGrantType()
        {
            _grantType = TokenGrantType.DeviceAuthorization;
            return this;
        }

        public ClientCredentials Build()
        {
            return new ClientCredentials()
            {
                Password = _password,
                Username = _username,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                GrantType = _grantType,
                Code = _code,
                DeviceCode = _deviceCode,
                RedirectUri = _redirectUri
            };
        }
    }
}
