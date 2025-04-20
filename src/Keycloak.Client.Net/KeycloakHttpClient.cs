using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Keycloak.Client.Net.AttackDetections;
using Keycloak.Client.Net.Groups;
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

        private readonly object _groupsLock = new object();
        private IGroups _groups;
        public IGroups Groups
        {
            get
            {
                if (_groups == null)
                {
                    lock (_groupsLock)
                    {
                        if (_groups == null)
                        {
                            _groups = new Group(this);
                        }
                    }
                }
                return _groups;
            }
            set
            {
                lock (_groupsLock)
                {
                    _groups = value;
                }
            }
        }

        public async Task<RestResponse> Execute()
        {
            if (_retryPolicy != null)
            {
                return await _retryPolicy.ExecuteAsync(() => _restClient.ExecuteAsync(Request));
            }

            return await _restClient.ExecuteAsync(Request);
        }

        public void AddQueryStrings(Dictionary<string, string> queryStrings)
        {
            if (queryStrings == null || queryStrings.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, string> queryString in queryStrings)
            {
                Request.AddQueryParameter(queryString.Key, queryString.Value);
            }
        }

        public void AddJsonBodyParameters(object obj)
        {
            Request.AddJsonBody(JsonSerializer.Serialize(obj));
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
