using Ardalis.Result;
using Keycloak.Client.Net;
using Keycloak.Client.Net.AttackDetections.Dtos.Interface;
using Keycloak.Client.Net.Builders;
using Keycloak.Client.Net.Console.Options;
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

Result<IStatusOfAUsernameInBruteForceDetectionDto> getStatusOfAUsernameInBruteForceDetection = await keycloakHttpClient.AttackDetections.GetStatusOfAUsernameInBruteForceDetection("e8244b4b-e504-41bc-8867-07c1038ccc54");
Result clearAnyUserLoginFailuresForTheUser = await keycloakHttpClient.AttackDetections.ClearAnyUserLoginFailuresForTheUser("e8244b4b-e504-41bc-8867-07c1038ccc54");
Result clearAnyUserLoginFailuresForAllUsers = await keycloakHttpClient.AttackDetections.ClearAnyUserLoginFailuresForAllUsers();

Console.ReadKey();
