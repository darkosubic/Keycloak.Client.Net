using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class UserConsentRepresentationDto
    {
        public UserConsentRepresentationDto()
        {
            GrantedClientScopes = new List<string>();
            GrantedRealmRoles = new List<string>();
        }

        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        [JsonPropertyName("grantedClientScopes")]
        public List<string> GrantedClientScopes { get; set; }

        [JsonPropertyName("createdDate")]
        public long? CreatedDate { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public bool? LastUpdatedDate { get; set; }

        [JsonPropertyName("grantedRealmRoles")]
        public List<string> GrantedRealmRoles { get; set; }
    }

}
