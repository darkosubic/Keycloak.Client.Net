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
        Task<Result> ClearAnyUserLoginFailuresForAllUsers();
        Task<Result> ClearAnyUserLoginFailuresForTheUser(string userId);
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
