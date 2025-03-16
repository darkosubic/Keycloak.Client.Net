using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Keycloak.Client.Net.AttackDetections;
using Polly.Retry;
using RestSharp;
using static Keycloak.Client.Net.Constants.RouteConstants;

namespace Keycloak.Client.Net
{
    public class KeycloakHttpClient : IKeycloakHttpClient
    {
        public readonly RestClient _restClient;
        public readonly AsyncRetryPolicy<RestResponse> _retryPolicy;
        private readonly ITokenProvider _tokenProvider;

        public string BaseUrl => RealmSettings.Url + "/" + RouteAdmin;

        public RealmSettings RealmSettings { get; private set; }

        public RestRequest Request { get; private set; }

        public KeycloakHttpClient(RealmSettings realmSettings, HttpClient httpClient, ITokenProvider tokenProvider, AsyncRetryPolicy<RestResponse> retryPolicy)
        {
            RealmSettings = realmSettings;

            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = httpClient.BaseAddress,
                ConfigureMessageHandler = _ => new HttpClientHandler()
            });

            _retryPolicy = retryPolicy;

            _tokenProvider = tokenProvider;
        }

        private readonly object _attackDetectionsLock = new object();
        private IAttackDetection _attackDetections;
        public IAttackDetection AttackDetections
        {
            get
            {
                if (_attackDetections == null)
                {
                    lock (_attackDetectionsLock)
                    {
                        if (_attackDetections == null)
                        {
                            _attackDetections = new AttackDetection(this);
                        }
                    }
                }
                return _attackDetections;
            }
            set
            {
                lock (_attackDetectionsLock)
                {
                    _attackDetections = value;
                }
            }
        }

        public async Task<RestResponse> Execute()
        {
            if (_retryPolicy != null)
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await _restClient.ExecuteAsync(Request);
                });
            }

            return await _restClient.ExecuteAsync(Request);
        }


        public async Task<RestRequest> Create(string endpoint, Method method)
        {
            if (Request == null)
            {
                Request = new RestRequest();
            }

            const string AuthorizationHeader = "Authorization";
            const string AcceptHeader = "Accept";
            const string AppJson = "application/json";
            const string Bearer = "Bearer";

            ClearRequest(AuthorizationHeader);

            Request.AddHeader(AuthorizationHeader, $"{Bearer} {await _tokenProvider.GetTokenAsync()}");
            Request.AddHeader(AcceptHeader, AppJson);

            Request.Method = method;

            Request.Resource = endpoint;

            return Request;
        }

        private void ClearRequest(string AuthorizationHeader)
        {
            Request.Parameters.RemoveParameter(AuthorizationHeader);

            List<Parameter> queryStringParams = Request.Parameters.Where(p => p.Type == ParameterType.QueryString).ToList();
            foreach (Parameter quersStringParam in queryStringParams)
            {
                Request.Parameters.RemoveParameter(quersStringParam);
            }

            Parameter bodyParam = Request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (bodyParam != null)
            {
                Request.Parameters.RemoveParameter(bodyParam);
            }
        }
    }
}
