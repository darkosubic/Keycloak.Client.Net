using System.Text.Json.Serialization;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;

namespace Keycloak.Client.Net.Groups.DTOs
{
    public class GetGroupMembersRequestDto : IGetGroupMembersRequestDto
    {
        /// <summary>
        /// If true</br>
        /// Only return basic information
        /// (only guaranteed to return id, username, created, first and last name, email, enabled state, email verification state, federation link, and access. Note that it means that namely user attributes, required actions, and not before are not returned.)
        /// </summary>
        [JsonPropertyName("briefRepresentation")]
        public bool? BriefRepresentation { get; set; }

        /// <summary>
        /// Default value is 0
        /// </summary>
        [JsonPropertyName("first")]
        public int? FirstResult { get; set; }

        /// <summary>
        /// Default value is 100
        /// </summary>
        [JsonPropertyName("max")]
        public int? MaxResults { get; set; }
    }
}
