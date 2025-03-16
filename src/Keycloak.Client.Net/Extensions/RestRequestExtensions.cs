using RestSharp;
using System;

namespace Keycloak.Client.Net.Extensions
{
    public static class RestRequestExtensions
    {
        private const string AuthorizationHeader = "Authorization";
        private const string Bearer = "Bearer";
        public static RestRequest AddBearerToken(this RestRequest request, string token)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Token cannot be null or empty.", nameof(token));

            return request.AddHeader(AuthorizationHeader, $"{Bearer} {token}");
        }
    }
}
