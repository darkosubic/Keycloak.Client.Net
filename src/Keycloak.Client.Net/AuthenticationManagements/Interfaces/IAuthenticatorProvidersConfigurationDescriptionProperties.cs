using System.Collections.Generic;

namespace Keycloak.Client.Net.AuthenticationManagements.Interfaces
{
    public interface IAuthenticatorProvidersConfigurationDescriptionProperties
    {
        string DefaultValue { get; set; }
        string HelpText { get; set; }
        string Name { get; set; }
        IEnumerable<string> Options { get; set; }
        string ProviderId { get; set; }
        bool? ReadOnly { get; set; }
        bool? Required { get; set; }
        bool? Secret { get; set; }
        string Type { get; set; }
    }
}
