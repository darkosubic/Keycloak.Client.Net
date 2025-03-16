namespace Keycloak.Client.Net.Console.Options
{
    public class KeycloakConfiguration
    {
        public const string Section = "KeycloakConfiguration";

        public string ServerUrl { get; set; } = string.Empty;
        public string RealmName { get; set; } = string.Empty;
    }
}
