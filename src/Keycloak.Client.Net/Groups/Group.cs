using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.Result;
using Keycloak.Client.Net.Extensions;
using Keycloak.Client.Net.Groups.DTOs;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;
using Keycloak.Client.Net.Users.DTOs;
using Keycloak.Client.Net.Users.Inerfaces;
using RestSharp;

namespace Keycloak.Client.Net.Groups
{
    public interface IGroups
    {
        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups/count ENDPOINT<br/>
        /// Documentation description: Returns the groups counts.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupsResource.java#L154">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupsResource.getGroupCount
        /// Auth requirements: QUERY_GROUPS || MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<IGetGroupCountResponseDto>> GetGroupCount(string search = null, string top = null);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups ENDPOINT<br/>
        /// Documentation description: Get group hierarchy. Only name and id are returned. subGroups are only returned when using the search or q parameter. If none of these parameters is provided, the top-level groups are returned without subGroups being filled.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupsResource.java#L90">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupsResource.getGroups
        /// Auth requirements: QUERY_GROUPS || MANAGE_USERS || VIEW_USERS
        /// </summary>
        Task<Result<List<GroupDto>>> GetGroups(GetGroupsDto searchParameters);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups/{group-id}/children ENDPOINT<br/>
        /// Documentation description: Return a paginated list of subgroups that have a parent group corresponding to the group on the URL<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L171">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupsResource.getSubGroups
        /// Auth requirements: MANAGE_AUTHORIZATION || VIEW_AUTHORIZATION
        /// </summary>
        Task<Result<List<GroupDto>>> GetSubGroups(string groupId, GetGroupSubgroupsDto searchParameters);

        /// <summary>
        /// Calls the POST /admin/realms/{realm}/groups/{group-id}/children ENDPOINT<br/>
        /// Documentation description: This will just set the parent if it exists.Create it and set the parent if the group doesn’t exist.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L202">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupsResource.addChild
        /// Auth requirements: MANAGE_USERS || Admin of this realm || MANAGE_SCOPE
        /// </summary>
        Task<Result> MoveToGroupOrCreateNewSubGroup(string groupId, GroupDto child);

        /// <summary>
        /// Calls the DELETE /admin/realms/{realm}/groups/{group-id} ENDPOINT<br/>
        /// Documentation description: N/A<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L158">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.deleteGroup
        /// Auth requirements: MANAGE_USERS || Admin of this realm || MANAGE_SCOPE
        /// </summary>
        Task<Result> DeleteGroup(string groupId);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups/{group-id} ENDPOINT<br/>
        /// Documentation description: N/A<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L101">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.getGroup
        /// Auth requirements: MANAGE_AUTHORIZATION || VIEW_AUTHORIZATION
        /// </summary>
        Task<Result<GroupDto>> GetGroupById(string groupId);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups/{group-id}/management/permissions ENDPOINT<br/>
        /// Documentation description: Return object stating whether client Authorization permissions have been initialized or not and a reference<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L327">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.getManagementPermissions
        /// Auth requirements: MANAGE_AUTHORIZATION || VIEW_AUTHORIZATION
        /// </summary>
        Task<Result<IClientManagementPermissionDto>> GetClientAuthorizationPermissions(string groupId);

        /// <summary>
        /// Calls the PUT /admin/realms/{realm}/groups/{group-id}/management/permissions ENDPOINT<br/>
        /// Documentation description: Return object stating whether client Authorization permissions have been initialized or not and a reference<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L360">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.setManagementPermissionsEnabled
        /// Feature requirement: feature.admin_fine_grained_authz
        /// Auth requirements: MANAGE_USERS || Admin of this realm || MANAGE_SCOPE
        /// </summary>
        Task<Result<IClientManagementPermissionDto>> UpdateClientAuthorizationPermissions(string groupId, IClientManagementPermissionDto clientManagementPermission);

        /// <summary>
        /// Calls the GET /admin/realms/{realm}/groups/{group-id}/members ENDPOINT<br/>
        /// Documentation description: Get users Returns a stream of users, filtered according to query parameters<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L300">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.getMembers
        /// Auth requirements: AdminRoles.MANAGE_USERS, VIEW_USERS || Admin of this realm || VIEW_MEMBERS_SCOPE, MANAGE_MEMBERS_SCOPE
        /// </summary>
        Task<Result<List<IUserRepresentationDto>>> GetGroupMembers(string groupId, IGetGroupMembersRequestDto getGroupMembers);

        /// <summary>
        /// Calls the PUT /admin/realms/{realm}/groups/{group-id} ENDPOINT<br/>
        /// Documentation description: Update group, ignores subgroups.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupResource.java#L120">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupResource.updateGroup
        /// Auth requirements: MANAGE_USERS || Admin of this realm || MANAGE_SCOPE
        /// </summary>
        Task<Result> UpdateGroup(string groupId, GroupDto getGroupMembers);

        /// <summary>
        /// Calls the POST /admin/realms/{realm}/groups ENDPOINT<br/>
        /// Documentation description: create or add a top level realm groupSet or create child.<br/>
        /// <see href="https://github.com/keycloak/keycloak/blob/main/services/src/main/java/org/keycloak/services/resources/admin/GroupsResource.java#L184">Github link</see> to the Api this method consumes <br/>
        /// Method it consumes: GroupsResource.addTopLevelGroup
        /// Auth requirements: MANAGE_USERS || Admin of this realm || MANAGE_SCOPE
        /// </summary>
        Task<Result> MoveToTopOrCreateNew(GroupDto getGroupMembers);
    }

    public class Group : ClientBase, IGroups
    {
        private readonly IKeycloakHttpClient _apiClient;
        private const string GroupsEndpoint = "groups";

        public Group(IKeycloakHttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result<IGetGroupCountResponseDto>> GetGroupCount(string search = null, string top = null)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/count", Method.Get);
            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<GetGroupCountResponseDto>(response.Content);
                }
                
                return HandleStandardErrorResponses(RequireListRoleRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IGetGroupCountResponseDto>();
            }

            void AddQueryString()
            {
                Dictionary<string, string> queryStrings = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(search))
                {
                    queryStrings.Add(nameof(search), search);
                }

                if (!string.IsNullOrEmpty(top))
                {
                    queryStrings.Add(nameof(top), top);
                }

                if (queryStrings.Count > 0)
                {
                    _apiClient.AddQueryStrings(queryStrings);
                }
            }
        }

        public async Task<Result<List<GroupDto>>> GetGroups(GetGroupsDto searchParameters)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}", Method.Get);
            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<List<GroupDto>>(response.Content);
                }

                return HandleStandardErrorResponses(RequireListRoleRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<List<GroupDto>>();
            }

            void AddQueryString()
            {
                if (searchParameters == null)
                {
                    return;
                }

                Dictionary<string, string> queryStrings = new Dictionary<string, string>();

                queryStrings["briefRepresentation"] = searchParameters.BriefRepresentation.ToString().ToLower();

                if (searchParameters.SearchByGroupName != null)
                {
                    queryStrings["search"] = searchParameters.SearchByGroupName;
                    queryStrings["exact"] = searchParameters.SearchByGroupNameByExactValue.ToString().ToString().ToLower();
                }

                if (searchParameters.FirstResult.HasValue)
                    queryStrings["first"] = searchParameters.FirstResult.Value.ToString();

                if (searchParameters.MaxResults.HasValue)
                    queryStrings["max"] = searchParameters.MaxResults.Value.ToString();

                queryStrings["populateHierarchy"] = searchParameters.PopulateHierarchy.ToString().ToLower();

                if (searchParameters.SearchByAtributeKeyAndValue != null)
                    queryStrings["q"] = searchParameters.SearchByAtributeKeyAndValue;

                _apiClient.AddQueryStrings(queryStrings);

            }
        }

        public async Task<Result<List<GroupDto>>> GetSubGroups(string groupId, GetGroupSubgroupsDto searchParameters)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}/children", Method.Get);
            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<List<GroupDto>>(response.Content);
                }

                return HandleStandardErrorResponses(RequireViewRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<List<GroupDto>>();
            }

            void AddQueryString()
            {
                Dictionary<string, string> queryStrings = new Dictionary<string, string>();

                queryStrings["briefRepresentation"] = searchParameters.BriefRepresentation.ToString().ToLower();

                if (searchParameters.SearchByGroupName != null)
                {
                    queryStrings["search"] = searchParameters.SearchByGroupName;
                    queryStrings["exact"] = searchParameters.SearchByGroupNameByExactValue.ToString().ToString().ToLower();
                }

                if (searchParameters.FirstResult.HasValue)
                    queryStrings["first"] = searchParameters.FirstResult.Value.ToString();

                if (searchParameters.MaxResults.HasValue)
                    queryStrings["max"] = searchParameters.MaxResults.Value.ToString();

                _apiClient.AddQueryStrings(queryStrings);

            }
        }

        public async Task<Result> MoveToGroupOrCreateNewSubGroup(string groupId, GroupDto child)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}/children", Method.Post);

            _apiClient.AddJsonBodyParameters(child);

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
                return ex.FailureFromException();
            }

        }

        public async Task<Result> DeleteGroup(string groupId)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}", Method.Delete);

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
                return ex.FailureFromException();
            }
        }

        public async Task<Result<GroupDto>> GetGroupById(string groupId)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<GroupDto>(response.Content);
                }

                return HandleStandardErrorResponses(RequireViewRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<GroupDto>();
            }
        }

        public async Task<Result<IClientManagementPermissionDto>> GetClientAuthorizationPermissions(string groupId)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}/management/permissions", Method.Get);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<ClientManagementPermissionDto>(response.Content);
                }

                return HandleStandardErrorResponses(RequireViewRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IClientManagementPermissionDto>();
            }
        }

        public async Task<Result<IClientManagementPermissionDto>> UpdateClientAuthorizationPermissions(string groupId, IClientManagementPermissionDto clientManagementPermission)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}/management/permissions", Method.Put);

            _apiClient.AddJsonBodyParameters(clientManagementPermission);

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    return JsonSerializer.Deserialize<ClientManagementPermissionDto>(response.Content);
                }

                return HandleStandardErrorResponses(RequireManageRequirement, response);
            }
            catch (Exception ex)
            {
                return ex.FailureFromException<IClientManagementPermissionDto>();
            }
        }

        public async Task<Result<List<IUserRepresentationDto>>> GetGroupMembers(string groupId, IGetGroupMembersRequestDto getGroupMembers)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}/members", Method.Get);

            AddQueryString();

            try
            {
                RestResponse response = await _apiClient.Execute();

                if (response.IsSuccessful)
                {
                    List<UserRepresentationDto> deserializedResult = JsonSerializer.Deserialize<List<UserRepresentationDto>>(response.Content);

                    return Result.Success(deserializedResult.Cast<IUserRepresentationDto>().ToList());
                }

                return HandleStandardErrorResponses(RequireViewMembersRequirement, response);

            }
            catch (Exception ex)
            {
                return ex.FailureFromException<List<IUserRepresentationDto>>();
            }

            void AddQueryString()
            {
                if (getGroupMembers == null)
                {
                    return;
                }
                Dictionary<string, string> queryStrings = new Dictionary<string, string>();

                if (getGroupMembers.BriefRepresentation.HasValue)
                    queryStrings["briefRepresentation"] = getGroupMembers.BriefRepresentation.ToString().ToLower();

                if (getGroupMembers.FirstResult.HasValue)
                    queryStrings["first"] = getGroupMembers.FirstResult.Value.ToString();

                if (getGroupMembers.MaxResults.HasValue)
                    queryStrings["max"] = getGroupMembers.MaxResults.Value.ToString();

                _apiClient.AddQueryStrings(queryStrings);

            }
        }

        public async Task<Result> UpdateGroup(string groupId, GroupDto getGroupMembers)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}/{groupId}", Method.Put);

            _apiClient.AddJsonBodyParameters(getGroupMembers);

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
                return ex.FailureFromException();
            }
        }

        public async Task<Result> MoveToTopOrCreateNew(GroupDto createGroup)
        {
            await _apiClient.Create($"{_apiClient.BaseUrl}/{_apiClient.RealmSettings.Name}/{GroupsEndpoint}", Method.Post);

            if (createGroup == null)
            {
                return Result.Error($"{nameof(createGroup)} cannot be null.");
            }

            _apiClient.AddJsonBodyParameters(createGroup);

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
                return ex.FailureFromException();
            }
        }

        private const string RequireViewRequirement = "MANAGE_AUTHORIZATION || VIEW_AUTHORIZATION";
        private const string RequireManageRequirement = "MANAGE_USERS|| Admin of this realm || MANAGE_SCOPE";
        private const string RequireViewMembersRequirement = "MANAGE_USERS || VIEW_USERS || Admin of this realm || VIEW_MEMBERS_SCOPE, MANAGE_MEMBERS_SCOPE";
        private const string RequireListRoleRequirement = "QUERY_GROUPS || MANAGE_USERS || VIEW_USERS";
    }
}
