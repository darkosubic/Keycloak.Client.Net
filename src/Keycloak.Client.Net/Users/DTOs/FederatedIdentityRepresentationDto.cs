using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class FederatedIdentityRepresentationDto
    {
        [JsonPropertyName("identityProvider")]
        public string IdentityProvider { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }
    }

}
