using System.Text.Json.Serialization;
using Keycloak.Client.Net.Users.Builders.GetUsersCount;
using Keycloak.Client.Net.Users.DTOs.Interfaces;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class GetUsersCountDto : IGetUsersCountDto
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("emailVerified")]
        public bool? EmailVerified { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("q")]
        public string SearchQuery { get; set; }

        public static GetUsersCountBuilder.IGetAllUsersCountBuilderStart GetAllUserCount() => GetUsersCountBuilder.GetAllUsersCountBuilder.Create();
        public static GetUsersCountBuilder.ICriteriaBuilderStart GetUserCountBySearchCriteria() => GetUsersCountBuilder.GetUserCountBySearchCriteria.Create();
        public static GetUsersCountBuilder.IGetUserCountBySearchTermBuilderStart GetUserCountBySearchTerm() => GetUsersCountBuilder.GetUserCountBySearchTermBuilder.Create();
        public static GetUsersCountBuilder.IUserExistsAndCanViewItBuilderStart DoesUserExistsAndCanViewIt() => GetUsersCountBuilder.UserExistsAndCanViewItBuilder.Create();
    }
}
