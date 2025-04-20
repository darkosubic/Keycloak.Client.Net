using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class ValidatorDto
    {
        public ValidatorDto()
        {
            Config = new Dictionary<string, string>();
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("config")]
        public Dictionary<string, string> Config { get; set; } 
    }

}
