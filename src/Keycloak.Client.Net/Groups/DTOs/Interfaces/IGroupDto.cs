using System.Collections.Generic;

namespace Keycloak.Client.Net.Groups.DTOs.Interfaces
{
    public interface IGroupDto
    {
        Dictionary<string, bool> Access { get; set; }
        Dictionary<string, List<string>> Attributes { get; set; }
        Dictionary<string, List<string>> ClientRoles { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        string ParentId { get; set; }
        string Path { get; set; }
        List<string> RealmRoles { get; set; }
        long? SubGroupCount { get; set; }
        IEnumerable<IGroupDto> SubGroups { get; set; }
    }
}
