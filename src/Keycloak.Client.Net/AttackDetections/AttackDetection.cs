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
        /// Documentation description: Clear any user login failures for all users This can release temporary disabled users<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L173">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.clearAllBruteForce
        /// Auth requirements: MANAGE_CLIENTS
        /// </summary>
        Task<Result> ClearAnyUserLoginFailuresForAllUsers();

        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/attack-detection/brute-force/users/{userId} ENDPOINT<br/>
        /// Documentation description: Clear any user login failures for the user This can release temporary disabled user<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L149">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.clearBruteForceForUser
        /// Auth requirements: MANAGE_CLIENTS
        /// </summary>
        Task<Result> ClearAnyUserLoginFailuresForTheUser(string userId);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/attack-detection/brute-force/users/{userId} ENDPOINT<br/>
        /// Documentation description: Get status of a username in brute force detection<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/AttackDetectionResource.java#L187">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: AttackDetectionResource.bruteForceUserStatus
        /// Auth requirements: MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<IStatusOfAUsernameInBruteForceDetectionDto>> GetStatusOfAUsernameInBruteForceDetection(string userId);
    }

    public class AttackDetection : IAttackDetection
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
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result<IStatusOfAUsernameInBruteForceDetectionDto>.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin or manage-users role.");
                }

                return Result<IStatusOfAUsernameInBruteForceDetectionDto>.Error($"Failed to get an OK status. Response message: {response.Content}");

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
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin or manage-users role.");
                }

                return Result.Error($"Failed to get an OK status. Response message: {response.Content}");

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
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin or manage-users role.");
                }

                return Result.Error($"Failed to get an OK status. Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<Result>();
            }
        }
    }

}
