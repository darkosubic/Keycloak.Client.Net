using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.Result;
using Keycloak.Client.Net.AuthenticationManagements.Dtos;
using Keycloak.Client.Net.AuthenticationManagements.Interfaces;
using Keycloak.Client.Net.Extensions;
using RestSharp;

namespace Keycloak.Client.Net.AuthenticationManagements
{
    public interface IAuthenticationManagement
    {
        Task<Result<IEnumerable<IGetAuthenticatorProvidersDto>>> GetAuthenticatorProviders();
        Task<Result<IEnumerable<IGetClientAuthenticatorProvidersDto>>> GetClientAuthenticatorProviders();
        Task<Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>> GetAuthenticatorProvidersConfigurationDescription(string providerId);
        Task<Result<IAuthenticatorConfigRepresentationDto>> GetAuthenticatorConfiguration(string id);
        Task<Result> CreateNewAuthenticatorConfiguration(IAuthenticatorConfigRepresentationDto authenticatorConfigRepresentation);
        Task<Result> DeleteAuthenticatorConfiguration(string id);
    }

    public class AuthenticationManagement : IAuthenticationManagement
    {
        private readonly IKeycloakHttpClient _apiClient;
        private const string AuthenticationManagementEndpoint = "authentication";
        private const string AuthenticatorConfigurationRoute = "config";

        public AuthenticationManagement(IKeycloakHttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result<IEnumerable<IGetAuthenticatorProvidersDto>>> GetAuthenticatorProviders()
        {
            const string GetAuthenticatorProvidersRoute = "authenticator-providers";
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{GetAuthenticatorProvidersRoute}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<List<GetAuthenticatorProvidersDto>>(response.Content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result<IEnumerable<IGetAuthenticatorProvidersDto>>.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result<IEnumerable<IGetAuthenticatorProvidersDto>>.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role..");
                }

                return Result<IEnumerable<IGetAuthenticatorProvidersDto>>.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IEnumerable<IGetAuthenticatorProvidersDto>>();
            }
        }

        public async Task<Result<IEnumerable<IGetClientAuthenticatorProvidersDto>>> GetClientAuthenticatorProviders()
        {
            const string GetClientAuthenticatorProvidersRoute = "client-authenticator-providers";
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{GetClientAuthenticatorProvidersRoute}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<List<GetClientAuthenticatorProvidersDto>>(response.Content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result<IEnumerable<IGetClientAuthenticatorProvidersDto>>.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result<IEnumerable<IGetClientAuthenticatorProvidersDto>>.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role..");
                }

                return Result<IEnumerable<IGetClientAuthenticatorProvidersDto>>.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IEnumerable<IGetClientAuthenticatorProvidersDto>>();
            }
        }

        public async Task<Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>> GetAuthenticatorProvidersConfigurationDescription(string providerId)
        {
            if (string.IsNullOrEmpty(providerId))
            {
                return Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>.Error($"{nameof(providerId)} parameter cannot be null or empty.");
            }
            const string GetAuthenticatorProvidersConfigurationRoute = "config-description";
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{GetAuthenticatorProvidersConfigurationRoute}/{providerId}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<GetAuthenticatorProvidersConfigurationDescriptionDto>(response.Content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>.Forbidden($"NotFound. Authenticator provider with id {providerId} does not exist.");
                }

                return Result<IGetAuthenticatorProvidersConfigurationDescriptionDto>.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IGetAuthenticatorProvidersConfigurationDescriptionDto>();
            }
        }


        public async Task<Result<IAuthenticatorConfigRepresentationDto>> GetAuthenticatorConfiguration(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Result<IAuthenticatorConfigRepresentationDto>.Error($"{nameof(id)} parameter cannot be null or empty.");
            }

            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{AuthenticatorConfigurationRoute}/{id}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<AuthenticatorConfigRepresentationDto>(response.Content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result<IAuthenticatorConfigRepresentationDto>.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin or try using admin-cli client.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result<IAuthenticatorConfigRepresentationDto>.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Result<IAuthenticatorConfigRepresentationDto>.Forbidden($"NotFound. Authenticator provider with id {id} does not exist.");
                }

                return Result<IAuthenticatorConfigRepresentationDto>.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IAuthenticatorConfigRepresentationDto>();
            }
        }

        public async Task<Result> CreateNewAuthenticatorConfiguration(IAuthenticatorConfigRepresentationDto authenticatorConfigRepresentation)
        {
            if (authenticatorConfigRepresentation == null)
            {
                return Result.Error($"{nameof(authenticatorConfigRepresentation)} parameter cannot be null or empty.");
            }

            Dictionary<string, object> bodyParameters =
                AuthenticatorConfigRepresentationToBodyParameters(authenticatorConfigRepresentation);

            JsonSerializer.Serialize(authenticatorConfigRepresentation);

            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{AuthenticatorConfigurationRoute}", Method.Post);
            _apiClient.AddJsonBody(authenticatorConfigRepresentation);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return Result.Success();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role.");
                }

                return Result.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        private static Dictionary<string, object> AuthenticatorConfigRepresentationToBodyParameters(IAuthenticatorConfigRepresentationDto authenticatorConfigRepresentation)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "Id", authenticatorConfigRepresentation.Id },
                { "alias", authenticatorConfigRepresentation.Alias }
            };
            foreach (var config in authenticatorConfigRepresentation.Config)
            {
                parameters.Add($"config[{config.Key}]", config.Value);
            }

            return parameters;
        }

        public async Task<Result> DeleteAuthenticatorConfiguration(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Result.Error($"{nameof(id)} parameter cannot be null or empty.");
            }

            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{AuthenticationManagementEndpoint}/{AuthenticatorConfigurationRoute}/{id}", Method.Delete);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return Result.Success();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Result.Unauthorized("Unauthorized. Make sure your token includes the an realm-admin.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return Result.Forbidden("Forbidden. Make sure your token includes the an realm-admin or manage-realm role.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Result.Forbidden($"NotFound. Authenticator provider with id {id} does not exist.");
                }

                return Result.Error($"Failed to get an OK status. Response code: {response.StatusCode} Response message: {response.Content}");

            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }
}
