using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.Client.Net.Constants;
using Keycloak.Client.Net.Models;
using RestSharp;

namespace Keycloak.Client.Net
{
    internal class TokenProvider : ITokenProvider
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ClientCredentials _clientCredentials;
        private readonly RealmSettings _realmSettings;
        private AccessTokenDto _latestToken;
        private DateTime _latestTokenExpirationTime;
        private DateTime _latestRefreshTokenExpirationTime;

        internal TokenProvider(ClientCredentials clientCredentials, RealmSettings realmSettings)
        {
            _clientCredentials = clientCredentials;
            _realmSettings = realmSettings;
            _latestToken = new AccessTokenDto();
            _semaphore = new SemaphoreSlim(1, 1);
        }

        private RestClient _accessTokenHttpClient;
        public async Task<string> GetTokenAsync()
        {
            if (IsAccessTokenValid() &&
                AccessTokenNotExpired())
            {
                return _latestToken.AccessToken;
            }

            await _semaphore.WaitAsync();
            try
            {
                if (_accessTokenHttpClient == null)
                {
                    _accessTokenHttpClient = new RestClient($"{_realmSettings.Url}/realms/{_realmSettings.Name}/protocol/openid-connect/token");
                }

                RestRequest request = AddAccessTokenRestRequestParemeters(_clientCredentials.GrantType);

                RestResponse response = await _accessTokenHttpClient.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _latestToken = JsonSerializer.Deserialize<AccessTokenDto>(response.Content);
                    _latestTokenExpirationTime = DateTime.Now.AddSeconds(_latestToken.ExpiresIn);
                    _latestRefreshTokenExpirationTime = DateTime.Now.AddSeconds(_latestToken.RefreshExpiresIn);

                    return _latestToken.AccessToken;
                }
                else
                {
                    ResetTokenData();

                    return string.Empty;
                }
            }
            catch
            {
                ResetTokenData();

                return string.Empty;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private RestRequest AddAccessTokenRestRequestParemeters(string grantType)
        {
            RestRequest request = new RestRequest();

            switch (grantType)
            {
                case TokenGrantType.ClientCredentials:
                {
                    request.AddParameter(TokenProviderParameterNameConstants.GrantType, TokenGrantType.ClientCredentials);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientId, _clientCredentials.ClientId);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientSecret, _clientCredentials.ClientSecret);
                }
                break;
                case TokenGrantType.AuthorizationCode:
                {
                    request.AddParameter(TokenProviderParameterNameConstants.GrantType, TokenGrantType.AuthorizationCode);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientId, _clientCredentials.ClientId);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientSecret, _clientCredentials.ClientSecret);
                    request.AddParameter(TokenProviderParameterNameConstants.Code, _clientCredentials.Code);
                    request.AddParameter(TokenProviderParameterNameConstants.RedirectUri, _clientCredentials.RedirectUri);
                }
                break;
                case TokenGrantType.Password:
                {
                    request.AddParameter(TokenProviderParameterNameConstants.GrantType, TokenGrantType.Password);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientId, _clientCredentials.ClientId);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientSecret, _clientCredentials.ClientSecret);
                    request.AddParameter(TokenProviderParameterNameConstants.Username, _clientCredentials.Username);
                    request.AddParameter(TokenProviderParameterNameConstants.Password, _clientCredentials.Password);
                }
                break;
                case TokenGrantType.DeviceAuthorization:
                {
                    request.AddParameter(TokenProviderParameterNameConstants.GrantType, TokenGrantType.DeviceAuthorization);
                    request.AddParameter(TokenProviderParameterNameConstants.ClientId, _clientCredentials.ClientId);
                    request.AddParameter(TokenProviderParameterNameConstants.DeviceCode, _clientCredentials.DeviceCode);
                }
                break;

                default:
                    throw new Exception("Invalid grant type");
            }

            return request;
        }

        private void ResetTokenData()
        {
            _latestToken = new AccessTokenDto();
            _latestTokenExpirationTime = DateTime.Now.AddSeconds(-1);
            _latestRefreshTokenExpirationTime = DateTime.Now.AddSeconds(-1);
        }

        private bool AccessTokenNotExpired()
        {
            return _latestToken.ExpiresIn > 1;
        }

        private bool IsAccessTokenValid()
        {
            return _latestToken != null &&
                   !string.IsNullOrEmpty(_latestToken.AccessToken);
        }

        private bool RefreshTokenNotExpired()
        {
            return _latestToken.ExpiresIn > 1;
        }

        private bool IsRefreshTokenValid()
        {
            return _latestToken != null &&
                   !string.IsNullOrEmpty(_latestToken.RefreshToken);
        }

        private RestClient _refreshTokenHttpClient;
        public async Task RefreshTokenAsync()
        {
            if (IsRefreshTokenValid() &&
                RefreshTokenNotExpired())
            {
                _ = await GetTokenAsync();

                return;
            }

            await _semaphore.WaitAsync();
            try
            {
                if (_refreshTokenHttpClient == null)
                {
                    _refreshTokenHttpClient = new RestClient($"{_realmSettings.Url}/realms/{_realmSettings.Name}/protocol/openid-connect/token");
                }

                RestRequest request = AddRefreshTokenRestRequestParemeters();

                RestResponse response = await _refreshTokenHttpClient.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _latestToken = JsonSerializer.Deserialize<AccessTokenDto>(response.Content);
                    _latestTokenExpirationTime = DateTime.Now.AddSeconds(_latestToken.ExpiresIn);
                    _latestRefreshTokenExpirationTime = DateTime.Now.AddSeconds(_latestToken.RefreshExpiresIn);
                }
                else
                {
                    ResetTokenData();
                }
            }
            catch
            {
                ResetTokenData();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private RestRequest AddRefreshTokenRestRequestParemeters()
        {
            RestRequest request = new RestRequest();

            request.AddParameter(TokenProviderParameterNameConstants.GrantType, TokenGrantType.RefreshToken);
            request.AddParameter(TokenProviderParameterNameConstants.ClientId, _clientCredentials.ClientId);
            request.AddParameter(TokenProviderParameterNameConstants.ClientSecret, _clientCredentials.ClientSecret);
            request.AddParameter(TokenProviderParameterNameConstants.RefreshToken, _latestToken.RefreshToken);

            return request;
        }
    }
}
