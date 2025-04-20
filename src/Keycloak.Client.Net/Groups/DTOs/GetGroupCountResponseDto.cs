using System.Text.Json.Serialization;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;

namespace Keycloak.Client.Net.Groups.DTOs
{
    public class GetGroupCountResponseDto : IGetGroupCountResponseDto
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }
    }
}
