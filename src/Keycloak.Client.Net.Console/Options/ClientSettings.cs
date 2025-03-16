namespace Keycloak.Client.Net.Console.Options
{
    public class ClientSettings
    {
        public const string Section = "Client";

        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Code { get; set; }
        public string? RedirectUri { get; set; }
        public string? DeviceCode { get; set; }
    }

}
