using Keycloak.Client.Net.AttackDetections;
using RestSharp;
using System.Threading.Tasks;

namespace Keycloak.Client.Net
{
    public interface IKeycloakHttpClient
    {
        string BaseUrl { get; }
        RealmSettings RealmSettings { get; }
        Task<RestRequest> Create(string endpoint, Method method);
        Task<RestResponse> Execute();

        IAttackDetection AttackDetections { get; set; }
    }
}
