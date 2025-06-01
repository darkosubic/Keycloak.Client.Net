using System;
using Ardalis.Result;
using System.Threading.Tasks;
using Keycloak.Client.Net.Extensions;
using RestSharp;
using Keycloak.Client.Net.Users.DTOs.Interfaces;
using System.Collections.Generic;
using Keycloak.Client.Net.Users.DTOs;
using Keycloak.Client.Net.Users.Inerfaces;
using System.Linq;
using System.Text.Json;

namespace Keycloak.Client.Net.Users
{
    public interface IUsers
    {
        /// <summary>
        /// Calls the GET /admin/realms/{realm}/users/count ENDPOINT<br/>
        /// Documentation description: Returns the number of users that match the given criteria.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/UsersResource.java#L400">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: UsersResource.getUsersCount
        /// Auth requirements: QUERY_USERS || MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<int>> GetUserCount(IGetUsersCountDto searchCriteria);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/users ENDPOINT<br/>
        /// Documentation description: Get users Returns a stream of users, filtered according to query parameters.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/UsersResource.java#L274">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: UsersResource.getUsers
        /// Auth requirements: QUERY_USERS || MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<List<IUserRepresentationDto>>> GetUsers(IGetUsersRequestDto searchCriteria);
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

        public async Task<Result<List<IUserRepresentationDto>>> GetUsers(IGetUsersRequestDto searchCriteria)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{UsersEndpoint}", Method.Get);
            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    List<UserRepresentationDto> deserializedResult = JsonSerializer.Deserialize<List<UserRepresentationDto>>(response.Content);

                    return Result.Success(deserializedResult.Cast<IUserRepresentationDto>().ToList());
                }

                return HandleStandardErrorResponses(RequireQueryRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<Result<List<IUserRepresentationDto>>>();
            }

            void AddQueryString()
            {
                if (searchCriteria == null)
                {
                    return;
                }
                Dictionary<string, string> queryStrings = new Dictionary<string, string>();

                if (searchCriteria.BriefRepresentation.HasValue)
                    queryStrings.Add("briefRepresentation", searchCriteria.BriefRepresentation.Value.ToString());

                if (!string.IsNullOrEmpty(searchCriteria.Email))
                    queryStrings.Add("email", searchCriteria.Email);

                if (searchCriteria.IsEmailVerified.HasValue)
                    queryStrings.Add("emailVerified", searchCriteria.IsEmailVerified.Value.ToString());

                if (searchCriteria.UserEnabled.HasValue)
                    queryStrings.Add("enabled", searchCriteria.UserEnabled.Value.ToString());

                if (searchCriteria.IsExact.HasValue)
                    queryStrings.Add("exact", searchCriteria.IsExact.Value.ToString());

                if (searchCriteria.PaginationOffset.HasValue)
                    queryStrings.Add("first", searchCriteria.PaginationOffset.Value.ToString());

                if (!string.IsNullOrEmpty(searchCriteria.FirstName))
                    queryStrings.Add("firstName", searchCriteria.FirstName);

                if (!string.IsNullOrEmpty(searchCriteria.IdpAlias))
                    queryStrings.Add("idpAlias", searchCriteria.IdpAlias);

                if (!string.IsNullOrEmpty(searchCriteria.IdpUserId))
                    queryStrings.Add("idpUserId", searchCriteria.IdpUserId);

                if (!string.IsNullOrEmpty(searchCriteria.LastName))
                    queryStrings.Add("lastName", searchCriteria.LastName);

                if (searchCriteria.MaxResultCount.HasValue)
                    queryStrings.Add("max", searchCriteria.MaxResultCount.Value.ToString());

                if (!string.IsNullOrEmpty(searchCriteria.Q))
                    queryStrings.Add("q", searchCriteria.Q);

                if (!string.IsNullOrEmpty(searchCriteria.Search))
                    queryStrings.Add("search", searchCriteria.Search);

                if (!string.IsNullOrEmpty(searchCriteria.Username))
                    queryStrings.Add("username", searchCriteria.Username);

                _apiClient.AddQueryStrings(queryStrings);

            }
        }
    }
}
