namespace Keycloak.Client.Net
{
    public sealed class ClientCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string DeviceCode { get; set; }
    }
}
