using Polly;
using Polly.Retry;
using RestSharp;
using System;
using System.Net.Http;

namespace Keycloak.Client.Net.Builders
{
    public class ApiClientBuilder
    {
        private readonly RealmSettingsBuilder _realmSettingsBuilder = RealmSettingsBuilder.New();
        private readonly ClientCredentialsBuilder _clientCredentialsBuilder = ClientCredentialsBuilder.New();
        private IHttpClientFactory _httpClientFactory;
        private ITokenProvider _tokenProvider;
        public AsyncRetryPolicy<RestResponse> _retryPolicy;

        public static ApiClientBuilder New() => new ApiClientBuilder();

        public ApiClientBuilder WithRealmSettings(Action<RealmSettingsBuilder> action)
        {
            action(_realmSettingsBuilder);

            return this;
        }

        public ApiClientBuilder WithBuiltInRetryPolicy(int numberOfRetries)
        {
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<RestResponse>(r => r.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //.RetryAsync(retryCount, async (result, retryAttempt, context) =>
                //{
                //    if (result.Result?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //    {
                //        await _tokenProvider.RefreshTokenAsync();
                //    }
                //});
                .WaitAndRetryAsync(
        retryCount: numberOfRetries, // Retry up to 3 times
        retryAttempt => TimeSpan.FromSeconds(3), // Set fixed delay of 3 seconds between retries
        async (result, timeSpan, retryCount, context) =>
        {
            if (result.Result?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _tokenProvider.RefreshTokenAsync();
            }
        });

            return this;
        }

        public ApiClientBuilder WithSelfImplementedRetryPolicy(AsyncRetryPolicy<RestResponse> retryPolicy)
        {
            _retryPolicy = retryPolicy;

            return this;
        }

        public ApiClientBuilder WithHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            return this;
        }

        public ApiClientBuilder WithBuiltInTokenProvider(Action<ClientCredentialsBuilder> action)
        {
            action(_clientCredentialsBuilder);

            return this;
        }

        public ApiClientBuilder WithSelfImplementedTokenProvider(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;

            return this;
        }

        public KeycloakHttpClient Build()
        {
            RealmSettings realmSetting = _realmSettingsBuilder.Build();
            ValidateRealmSetting(realmSetting);

            ValidateHttpClientFactory(_httpClientFactory);
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(realmSetting.Url);

            if (_tokenProvider == null)
            {
                _tokenProvider = new TokenProvider(_clientCredentialsBuilder.Build(), realmSetting);
            }

            return new KeycloakHttpClient(realmSetting, httpClient, _tokenProvider, _retryPolicy);
        }

        private void ValidateHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
            {
                throw new InvalidOperationException($"You have to provide an HttpClient factory using {nameof(WithHttpClientFactory)} method.");
            }
        }

        private void ValidateRealmSetting(RealmSettings realmSetting)
        {
            if (realmSetting == null)
            {
                throw new InvalidOperationException($"You have to set up Realm settings using {nameof(WithRealmSettings)}.");
            }

            if (string.IsNullOrEmpty(realmSetting.Name))
            {
                throw new InvalidOperationException("You have to provide a Realm name.");
            }

            if (string.IsNullOrEmpty(realmSetting.Url))
            {
                throw new InvalidOperationException("You have to provide a Keycloak Server Url.");
            }
        }
    }
}
