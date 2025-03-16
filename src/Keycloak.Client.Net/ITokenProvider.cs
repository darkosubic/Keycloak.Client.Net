using System.Threading.Tasks;

namespace Keycloak.Client.Net
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
        Task RefreshTokenAsync();
    }

}
