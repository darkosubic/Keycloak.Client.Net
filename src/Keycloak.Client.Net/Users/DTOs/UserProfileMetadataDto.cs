using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class UserProfileMetadataDto
    {
        public UserProfileMetadataDto()
        {
            Attributes = new List<UserProfileAttributeMetadataDto>();
            Groups = new List<UserProfileAttributeGroupMetadata>();
        }

        [JsonPropertyName("attributes")]
        public List<UserProfileAttributeMetadataDto> Attributes { get; set; }

        [JsonPropertyName("groups")]
        public List<UserProfileAttributeGroupMetadata> Groups { get; set; }
    }

}
