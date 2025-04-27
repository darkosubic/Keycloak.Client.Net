using System.Collections.Generic;
using System.Text.Json.Serialization;
using Keycloak.Client.Net.AuthenticationManagements.Interfaces;

namespace Keycloak.Client.Net.AuthenticationManagements.Dtos
{
    public class GetAuthenticatorProvidersConfigurationDescriptionDto : IGetAuthenticatorProvidersConfigurationDescriptionDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null;

        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; } = null;

        [JsonPropertyName("helpText")]
        public string HelpText { get; set; } = null;

        [JsonPropertyName("properties")]
        public IEnumerable<AuthenticatorProvidersConfigurationDescriptionProperties> Properties { get; set; } = new List<AuthenticatorProvidersConfigurationDescriptionProperties>();
    }

    public class AuthenticatorProvidersConfigurationDescriptionProperties : IAuthenticatorProvidersConfigurationDescriptionProperties
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null;

        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; } = null;

        [JsonPropertyName("helpText")]
        public string HelpText { get; set; } = null;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null;

        [JsonPropertyName("defaultValue")]
        public string DefaultValue { get; set; }

        [JsonPropertyName("options")]
        public IEnumerable<string> Options { get; set; } = new List<string>();

        [JsonPropertyName("secret")]
        public bool? Secret { get; set; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("readOnly")]
        public bool? ReadOnly { get; set; }
    }
}
