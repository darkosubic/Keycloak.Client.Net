using System.Text.Json.Serialization;
using Keycloak.Client.Net.Users.DTOs.Interfaces;
using Keycloak.Client.Net.Users.Builders.GetUsersByIdsBuilder;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class GetsUserRequestDto : IGetUsersRequestDto
    {
        [JsonPropertyName("briefRepresentation")]
        public bool? BriefRepresentation { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("emailVerified")]
        public bool? IsEmailVerified { get; set; }

        [JsonPropertyName("enabled")]
        public bool? UserEnabled { get; set; }

        [JsonPropertyName("exact")]
        public bool? IsExact { get; set; }

        [JsonPropertyName("first")]
        public int? PaginationOffset { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("idpAlias")]
        public string IdpAlias { get; set; }

        [JsonPropertyName("idpUserId")]
        public string IdpUserId { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("max")]
        public int? MaxResultCount { get; set; }

        [JsonPropertyName("q")]
        public string Q { get; set; }

        [JsonPropertyName("search")]
        public string Search { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }


        public static GetUsersBuilder.IGetUsersByIdsBuilderStart GetUsersByIds() => GetUsersBuilder.GetUsersByIdsBuilder.Create();
        public static GetUsersBuilder.IGetUsersByFilteringBuilderStart GetUsersByFilters() => GetUsersBuilder.GetUsersByFilteringBuilder.Create();
        public static GetUsersBuilder.IGetUsersBySearchTermBuilderStart GetUsersBySearchTerm() => GetUsersBuilder.GetUsersBySearchTermBuilder.Create();
    }
}
