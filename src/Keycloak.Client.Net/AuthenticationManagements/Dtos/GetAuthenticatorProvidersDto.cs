using System.Text.Json.Serialization;
using Keycloak.Client.Net.AuthenticationManagements.Interfaces;

namespace Keycloak.Client.Net.AuthenticationManagements.Dtos
{
    public class GetAuthenticatorProvidersDto : IGetAuthenticatorProvidersDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
