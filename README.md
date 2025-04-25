# Keycloak.Client.Net
 A resilient and testable .NET Standard 2.0 library for interacting with Keycloak’s Admin REST API, using RestSharp, supporting dependency injection via interfaces, Polly for fault handling, and the Result pattern for predictable outcomes.

**Keycloak.Client.Net** library is still in the development phase, and **15** out of **373** endpoint clients are implemented at this moment.

## Usage
As this is a .NET Standard 2.0 library, it is possible to use this library with the following .NET Implementations:
| .NET Implementation       | Version Support                                        |
|---------------------------|--------------------------------------------------------|
| .NET and .NET Core         | 2.0, 2.1, 2.2, 3.0, 3.1, 5.0, 6.0, 7.0, 8.0, 9.0       |
| .NET Framework             | 4.6.1 2, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8, 4.8.1         |

This repo also contains an example of the [Console app](/src/Keycloak.Client.Net.Console/Program.cs), which consumes the **Keycloak.Client.Net** library.

## Getting started
The heart of this library is the [KeycloakHttpClient](/src/Keycloak.Client.Net/KeycloakHttpClient.cs) class.

From this class, we are calling **Keycloak’s Admin REST API** endpoints as they are organized on the [**Keycloak Admin REST API** documentation page](https://www.keycloak.org/docs-api/latest/rest-api/index.html).

Each resource defined on the **Keycloak Admin REST API** documentation page is placed in a property for easier navigation. 

For example, if we want to make a **GET** request to the **/admin/realms/{realm}/attack-detection/brute-force/users/{userId}** endpoint, we would instantiate KeycloakHttpClient and use **AttackDetections** property to select the desired method and forward needed parameters to it.

```csharp
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

Result<IStatusOfAUsernameInBruteForceDetectionDto> getStatusOfAUsernameInBruteForceDetection = await keycloakHttpClient.AttackDetections.GetStatusOfAUsernameInBruteForceDetection("users-id");
```

There are a few things to notice:
* Even though we are using a builder pattern to build KeycloakHttpClient, you are not required to use it.
* You have to provide HttpClientFactory and RealmSettings.
* You can use your implementation of the Token provider or use the built-in one.
* You can use your implementation of Polly retry policy, use the built-in one or choose not to use Polly at all.


# Implemented so far:
| Resource                    | Implemented |
|-----------------------------|------------|
| Attack Detection             | ✅         |
| Authentication Management    | ❌         |
| Client Attribute Certificate  | ❌         |
| Client Initial Access        | ❌         |
| Client Registration Policy   | ❌         |
| Client Role Mappings         | ❌         |
| Client Scopes                | ❌         |
| Clients                      | ❌         |
| Component                    | ❌         |
| default                      | ❌         |
| Groups                       | ✅         |
| Identity Providers           | ❌         |
| Key                          | ❌         |
| Organizations                | ❌         |
| Protocol Mappers             | ❌         |
| Realms Admin                 | ❌         |
| Role Mapper                  | ❌         |
| Roles                        | ❌         |
| Roles (by ID)                | ❌         |
| Scope Mappings               | ❌         |
| Users                        | ⏳ In Progress |
