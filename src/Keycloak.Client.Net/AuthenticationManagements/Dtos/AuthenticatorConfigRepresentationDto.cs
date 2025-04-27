using System.Collections.Generic;
using System.Text.Json.Serialization;
using Keycloak.Client.Net.AuthenticationManagements.Interfaces;

namespace Keycloak.Client.Net.AuthenticationManagements.Dtos
{
    public class AuthenticatorConfigRepresentationDto : IAuthenticatorConfigRepresentationDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null;

        [JsonPropertyName("alias")]
        public string Alias { get; set; } = null;

        [JsonPropertyName("config")]
        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>();
    }
}
