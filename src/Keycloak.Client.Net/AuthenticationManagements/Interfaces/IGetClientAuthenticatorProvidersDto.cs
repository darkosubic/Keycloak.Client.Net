namespace Keycloak.Client.Net.AuthenticationManagements.Interfaces
{
    public interface IGetClientAuthenticatorProvidersDto
    {
        string Id { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        bool SupportsSecret { get; set; }
    }
}
