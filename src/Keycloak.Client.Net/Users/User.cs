using System;
using Ardalis.Result;
using System.Threading.Tasks;
using Keycloak.Client.Net.Extensions;
using RestSharp;
using Keycloak.Client.Net.Users.DTOs.Interfaces;
using System.Collections.Generic;

namespace Keycloak.Client.Net.Users
{
    public interface IUsers
    {
        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/attack-detection/brute-force/users ENDPOINT<br/>
        /// Documentation description: Clear any user login failures for all users This can release temporary disabled users<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/UsersResource.java#L400">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.getUsersCount
        /// Auth requirements: QUERY_USERS || MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<int>> GetUserCount(IGetUsersCountDto searchCriteria);
    }
    internal class User : ClientBase, IUsers
    {
        private readonly IKeycloakHttpClient _apiClient;
        private const string UsersEndpoint = "/users";
        private const string RequireQueryRequirement = "QUERY_USERS || MANAGE_USERS || VIEW_USERS";

        public User(IKeycloakHttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result<int>> GetUserCount(IGetUsersCountDto searchCriteria)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{UsersEndpoint}/count", Method.Get);
            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return int.Parse(response.Content);
                }

                return HandleStandardErrorResponses(RequireQueryRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<Result<int>>();
            }

            void AddQueryString()
            {
                var queryStrings = new Dictionary<string, string>();

                if (searchCriteria.Search != null)
                    queryStrings["search"] = searchCriteria.Search;

                if (searchCriteria.LastName != null)
                    queryStrings["lastName"] = searchCriteria.LastName;

                if (searchCriteria.FirstName != null)
                    queryStrings["firstName"] = searchCriteria.FirstName;

                if (searchCriteria.Email != null)
                    queryStrings["email"] = searchCriteria.Email;

                if (searchCriteria.EmailVerified != null)
                    queryStrings["emailVerified"] = searchCriteria.EmailVerified.Value.ToString().ToLower();

                if (searchCriteria.Username != null)
                    queryStrings["username"] = searchCriteria.Username;

                if (searchCriteria.Enabled != null)
                    queryStrings["enabled"] = searchCriteria.Enabled.Value.ToString().ToLower();

                if (searchCriteria.SearchQuery != null)
                    queryStrings["q"] = searchCriteria.SearchQuery;

                _apiClient.AddQueryStrings(queryStrings);

            }
        }
    }
}
