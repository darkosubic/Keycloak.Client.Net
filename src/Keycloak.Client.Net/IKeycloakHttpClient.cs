using Keycloak.Client.Net.AttackDetections;
using Keycloak.Client.Net.Groups;
using Keycloak.Client.Net.Users;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keycloak.Client.Net
{
    public interface IKeycloakHttpClient
    {
        string BaseUrl { get; }
        RealmSettings RealmSettings { get; }
        void AddQueryStrings(Dictionary<string, string> queryStrings);
        void AddJsonBodyParameters(object obj);
        Task<RestRequest> Create(string endpoint, Method method);
        Task<RestResponse> Execute();

        IAttackDetection AttackDetections { get; set; }
        IGroups Groups { get; set; }
        IUsers Users { get; set; }
    }
}
