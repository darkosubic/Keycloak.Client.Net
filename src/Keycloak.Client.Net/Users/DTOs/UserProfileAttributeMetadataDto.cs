using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class UserProfileAttributeMetadataDto
    {
        public UserProfileAttributeMetadataDto()
        {
            Annotations = new Dictionary<string, string>();
            Validators = new List<ValidatorDto>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("readOnly")]
        public bool? ReadOnly { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, string> Annotations { get; set; }

        [JsonPropertyName("validators")]
        public List<ValidatorDto> Validators { get; set; } 

        [JsonPropertyName("multivalued")]
        public bool? Multivalued { get; set; }
    }

}
