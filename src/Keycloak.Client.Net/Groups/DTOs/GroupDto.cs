using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using Keycloak.Client.Net.Groups.Builders;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;

namespace Keycloak.Client.Net.Groups.DTOs
{
    public class GroupDto : IGroupDto
    {
        public GroupDto()
        {
            SubGroupsConcrete = new List<GroupDto>();
            Attributes = new Dictionary<string, List<string>>();
            RealmRoles = new List<string>();
            ClientRoles = new Dictionary<string, List<string>>();
            Access = new Dictionary<string, bool>();
        }

        public static GroupDtoBuilder.ICreateNewGroupBuilderStart CreateNewGroup() => GroupDtoBuilder.CreateNewGroupBuilder.Create();
        public static GroupDtoBuilder.ICreateNewChildBuilderStart CreateNewChild() => GroupDtoBuilder.CreateNewChildBuilder.Create();
        public static GroupDtoBuilder.IMoveChildGroupToTopBuilderStart MoveChildGroupToTop() => GroupDtoBuilder.MoveChildGroupToTopBuilder.Create();
        public static GroupDtoBuilder.IMoveToAnotherGroupBuilderStart MoveToAnotherGroup() => GroupDtoBuilder.MoveToAnotherGroupBuilder.Create();
        public static GroupDtoBuilder.IUpdateGroupBuilderStart UpdateGroup() => GroupDtoBuilder.UpdateGroupBuilder.Create();

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("parentId")]
        public string ParentId { get; set; }

        [JsonPropertyName("subGroupCount")]
        public long? SubGroupCount { get; set; }

        [JsonPropertyName("subGroups")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public List<GroupDto> SubGroupsConcrete { get; set; }

        [JsonIgnore]
        public IEnumerable<IGroupDto> SubGroups
        {
            get => SubGroupsConcrete;
            set => SubGroupsConcrete = value.Cast<GroupDto>().ToList(); 
        }

        [JsonPropertyName("attributes")]
        public Dictionary<string, List<string>> Attributes { get; set; }

        [JsonPropertyName("realmRoles")]
        public List<string> RealmRoles { get; set; }

        [JsonPropertyName("clientRoles")]
        public Dictionary<string, List<string>> ClientRoles { get; set; }

        [JsonPropertyName("access")]
        public Dictionary<string, bool> Access { get; set; }
    }
}

