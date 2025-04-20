using System.Collections.Generic;

namespace Keycloak.Client.Net.Groups.DTOs.Interfaces
{
    public interface IClientManagementPermissionDto
    {
        bool Enabled { get; set; }
        string Resource { get; set; }
        Dictionary<string, string> ScopePermissions { get; set; }
    }
}
