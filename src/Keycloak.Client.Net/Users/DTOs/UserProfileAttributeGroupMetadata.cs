using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class UserProfileAttributeGroupMetadata
    {
        public UserProfileAttributeGroupMetadata()
        {
            Annotations = new Dictionary<string, string>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayHeader")]
        public string DisplayHeader { get; set; }

        [JsonPropertyName("displayDescription")]
        public string DisplayDescription { get; set; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, string> Annotations { get; set; } 
    }

}
