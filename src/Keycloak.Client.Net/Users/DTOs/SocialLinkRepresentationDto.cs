using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class SocialLinkRepresentationDto
    {
        [JsonPropertyName("socialProvider")]
        public string SocialProvider { get; set; }

        [JsonPropertyName("socialUserId")]
        public string SocialUserId { get; set; }

        [JsonPropertyName("socialUsername")]
        public string SocialUsername { get; set; }
    }

}
