using Ardalis.Result;
using Keycloak.Client.Net.AttackDetections.Dtos;
using Keycloak.Client.Net.AttackDetections.Dtos.Interfaces;
using Keycloak.Client.Net.Extensions;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Keycloak.Client.Net.AttackDetections
{
    public interface IAttackDetection
    {
        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/attack-detection/brute-force/users ENDPOINT<br/>
        /// Documentation description: Returns the groups counts.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L173">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.clearAllBruteForce
        /// Auth requirements: MANAGE_CLIENTS
        /// </summary>
        Task<Result> ClearAnyUserLoginFailuresForAllUsers();

        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/attack-detection/brute-force/users ENDPOINT<br/>
        /// Documentation description: Returns the groups counts.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L149">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.clearBruteForceForUser
        /// Auth requirements: MANAGE_CLIENTS
        /// </summary>
        Task<Result> ClearAnyUserLoginFailuresForTheUser(string userId);

        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/attack-detection/brute-force/users ENDPOINT<br/>
        /// Documentation description: Returns the groups counts.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L187">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.bruteForceUserStatus
        /// Auth requirements: MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<IStatusOfAUsernameInBruteForceDetectionDto>> GetStatusOfAUsernameInBruteForceDetection(string userId);
    }

    public class AttackDetection : ClientBase, IAttackDetection
    {
        private readonly IKeycloakHttpClient _apiClient;
        private const string AtackDetectionEndpoint = "/attack-detection/brute-force/users";

        public AttackDetection(IKeycloakHttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result<IStatusOfAUsernameInBruteForceDetectionDto>> GetStatusOfAUsernameInBruteForceDetection(string userId)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AtackDetectionEndpoint}/{userId}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<StatusOfAUsernameInBruteForceDetectionDto>(response.Content);
                }

                return HandleStandardErrorResponses(RequireViewRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IStatusOfAUsernameInBruteForceDetectionDto>();
            }
        }

        public async Task<Result> ClearAnyUserLoginFailuresForTheUser(string userId)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AtackDetectionEndpoint}/{userId}", Method.Delete);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return Result.Success();
                }

                return HandleStandardErrorResponses(RequireManageRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<Result>();
            }
        }

        public async Task<Result> ClearAnyUserLoginFailuresForAllUsers()
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AtackDetectionEndpoint}", Method.Delete);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return Result.Success();
                }

                return HandleStandardErrorResponses(RequireManageRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<Result>();
            }
        }

        private const string RequireManageRequirement = "MANAGE_CLIENTS";
        private const string RequireViewRequirement = "MANAGE_USERS || VIEW_USERS";
    }

}
