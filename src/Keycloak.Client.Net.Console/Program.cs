using Ardalis.Result;
using Keycloak.Client.Net;
using Keycloak.Client.Net.AttackDetections.Dtos.Interfaces;
using Keycloak.Client.Net.Builders;
using Keycloak.Client.Net.Console.Options;
using Keycloak.Client.Net.Groups;
using Keycloak.Client.Net.Groups.DTOs;
using Keycloak.Client.Net.Groups.DTOs.Interfaces;
using Keycloak.Client.Net.Users.DTOs;
using Keycloak.Client.Net.Users.DTOs.Interfaces;
using Keycloak.Client.Net.Users.Inerfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<KeycloakConfiguration>(context.Configuration.GetSection(KeycloakConfiguration.Section));
                services.Configure<ClientSettings>(context.Configuration.GetSection(ClientSettings.Section));
                services.AddHttpClient();
            })
            .Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
KeycloakConfiguration keycloakConfig = config.GetSection(KeycloakConfiguration.Section).Get<KeycloakConfiguration>()!;
ClientSettings clientSettings = config.GetSection(ClientSettings.Section).Get<ClientSettings>()!;

IHttpClientFactory httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();
HttpClient client = httpClientFactory.CreateClient();

IKeycloakHttpClient keycloakHttpClient = ApiClientBuilder.New()
    .WithHttpClientFactory(httpClientFactory)
    .WithRealmSettings(client => client
        .ServerUrl(keycloakConfig.ServerUrl)
        .RealmName(keycloakConfig.RealmName))
    .WithBuiltInTokenProvider(tokenProvider => tokenProvider
        .UsePasswordGrantType()
        .ClientId(clientSettings.ClientId)
        .ClientSecret(clientSettings.ClientSecret)
        .Username(clientSettings.Username)
        .Password(clientSettings.Password))
    .WithBuiltInRetryPolicy(3)
    .Build();

#region AttackDetections
//Result<IStatusOfAUsernameInBruteForceDetectionDto> getStatusOfAUsernameInBruteForceDetection = await keycloakHttpClient.AttackDetections.GetStatusOfAUsernameInBruteForceDetection("e8244b4b-e504-41bc-8867-07c1038ccc54");
//Result clearAnyUserLoginFailuresForTheUser = await keycloakHttpClient.AttackDetections.ClearAnyUserLoginFailuresForTheUser("e8244b4b-e504-41bc-8867-07c1038ccc54");
//Result clearAnyUserLoginFailuresForAllUsers = await keycloakHttpClient.AttackDetections.ClearAnyUserLoginFailuresForAllUsers();
#endregion

#region Groups
//var attributes = new Dictionary<string, List<string>>
//{
//    { "hello", new List<string>() { "world" } }
//};

//string topLevelGroupId = "6d159831-beb8-4885-8bb4-066badbe70d4";
//Result newTopLevelGroup1 = await keycloakHttpClient.Groups.MoveToTopOrCreateNew(GroupDto.CreateNewGroup().WithName("TestGroup1").WithAttributes(attributes).Build());

//Result moveGroup = await keycloakHttpClient.Groups.MoveToTopOrCreateNew(GroupDto.MoveChildGroupToTop().WithId(topLevelGroupId).Build());
//Result<GroupDto> dgroup = await keycloakHttpClient.Groups.GetGroupById(topLevelGroupId);
//Result<List<GroupDto>> groupsChildern = await keycloakHttpClient.Groups.GetSubGroups(topLevelGroupId, new GetGroupSubgroupsDto() { BriefRepresentation = false });
//Result moveChildToAnotherGroup = await keycloakHttpClient.Groups.MoveToGroupOrCreateNewSubGroup("09fdc64b-9ca8-4c19-b080-1ddc65eae5d2", GroupDto.MoveToAnotherGroup().WithId(topLevelGroupId).Build());
//Result createNewChild = await keycloakHttpClient.Groups.MoveToGroupOrCreateNewSubGroup("698a34e2-5243-4307-a43b-72760e406544", GroupDto.CreateNewChild().WithName("NewChild2").WithAttributes(attributes).Build());
//Result<List<IUserRepresentationDto>> getGroupMembers = await keycloakHttpClient.Groups.GetGroupMembers(topLevelGroupId, new GetGroupMembersRequestDto() { BriefRepresentation = false });
//Result<IGetGroupCountResponseDto> groupCount = await keycloakHttpClient.Groups.GetGroupCount("Test", null);
//Result<List<GroupDto>> groupsByGroupName = await keycloakHttpClient.Groups.GetGroups(new GetGroupsDto() { SearchByGroupName = "TestGroup1", PopulateHierarchy = false });
//Result<List<GroupDto>> fullGroupDetails = await keycloakHttpClient.Groups.GetGroups(new GetGroupsDto() { BriefRepresentation = false, PopulateHierarchy = true });
//Result<List<GroupDto>> groupsByAttribute = await keycloakHttpClient.Groups.GetGroups(new GetGroupsDto() { SearchByAtributeKeyAndValue = "hello:world", PopulateHierarchy = true });
//Result<List<GroupDto>> groupsWithoutHierarchy = await keycloakHttpClient.Groups.GetGroups(new GetGroupsDto() { PopulateHierarchy = false });

//Result updatedGroup = await keycloakHttpClient.Groups.UpdateGroup(topLevelGroupId, GroupDto.UpdateGroup().WithId(topLevelGroupId).WithName("Updated Name").WithAttributes(attributes).Build());

//Result<IClientManagementPermissionDto> enableGroupPermisssions = await keycloakHttpClient.Groups.UpdateClientAuthorizationPermissions(topLevelGroupId, new ClientManagementPermissionDto() { Enabled = true });
//Result<IClientManagementPermissionDto> disableGroupPermisssions = await keycloakHttpClient.Groups.UpdateClientAuthorizationPermissions(topLevelGroupId, new ClientManagementPermissionDto() { Enabled = false });
//Result<IClientManagementPermissionDto> getGroupPermisssions = await keycloakHttpClient.Groups.GetClientAuthorizationPermissions(topLevelGroupId);
//Result deleteGroup = await keycloakHttpClient.Groups.DeleteGroup(topLevelGroupId);
#endregion

Result<int> filteredUserCount = await keycloakHttpClient.Users.GetUserCount(GetUsersCountDto.GetUserCountBySearchTerm().WithSearchTerm("test").Build());
Result<int> allUserCount = await keycloakHttpClient.Users.GetUserCount(GetUsersCountDto.GetAllUserCount().GetAll().Build());
Result<int> userCountByEmail = await keycloakHttpClient.Users.GetUserCount(GetUsersCountDto.GetUserCountBySearchCriteria().WithEmail("test@user.com").Build());

Result<List<IUserRepresentationDto>> usersById1 = await keycloakHttpClient.Users.GetUsers(GetsUserRequestDto.GetUsersByIds().WithASingleUserId("someId").Build());
Result<List<IUserRepresentationDto>> usersByFilters = await keycloakHttpClient.Users.GetUsers(GetsUserRequestDto.GetUsersByFilters()
    .WithEmailVerified(true)
    .WithBriefRepresentation(false)
    .WithEmail("Dsubic@gmail.com")
    .WithExact(true)
    /*.WithIdpAlias()*/.Build());

Result<List<IUserRepresentationDto>> usersBySearchTerm1 = await keycloakHttpClient.Users.GetUsers(GetsUserRequestDto.GetUsersBySearchTerm().WithSearch("test").WithExact(true).Build());
Result<List<IUserRepresentationDto>> usersBySearchTerm2 = await keycloakHttpClient.Users.GetUsers(GetsUserRequestDto.GetUsersBySearchTerm().WithInfixSearch("test").WithExact(true).Build());
Result<List<IUserRepresentationDto>> usersBySearchTerm3 = await keycloakHttpClient.Users.GetUsers(GetsUserRequestDto.GetUsersBySearchTerm().WithInfixSearch("est").WithExact(true).Build());


Console.ReadKey();
