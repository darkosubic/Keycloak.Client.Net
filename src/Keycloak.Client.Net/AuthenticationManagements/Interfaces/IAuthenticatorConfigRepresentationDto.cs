using System.Collections.Generic;

namespace Keycloak.Client.Net.AuthenticationManagements.Interfaces
{
    public interface IAuthenticatorConfigRepresentationDto
    {
        string Alias { get; set; }
        Dictionary<string, string> Config { get; set; }
        string Id { get; set; }
    }
}