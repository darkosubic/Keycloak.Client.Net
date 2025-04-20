using System.Collections.Generic;
using System.Text.Json.Serialization;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;

namespace Keycloak.Client.Net.Groups.DTOs
{
    public class ClientManagementPermissionDto : IClientManagementPermissionDto
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("resource")]
        public string Resource { get; set; }

        [JsonPropertyName("scopePermissions")]
        public Dictionary<string, string> ScopePermissions { get; set; } = new Dictionary<string, string>();
    }
}
