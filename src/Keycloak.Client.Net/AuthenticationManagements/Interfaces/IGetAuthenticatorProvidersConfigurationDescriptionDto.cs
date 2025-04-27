using System.Collections.Generic;
using Keycloak.Client.Net.AuthenticationManagements.Dtos;

namespace Keycloak.Client.Net.AuthenticationManagements.Interfaces
{
    public interface IGetAuthenticatorProvidersConfigurationDescriptionDto
    {
        string HelpText { get; set; }
        string Name { get; set; }
        IEnumerable<AuthenticatorProvidersConfigurationDescriptionProperties> Properties { get; set; }
        string ProviderId { get; set; }
    }
}
