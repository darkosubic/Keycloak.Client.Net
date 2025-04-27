namespace Keycloak.Client.Net.AuthenticationManagements.Interfaces
{
    public interface IGetAuthenticatorProvidersDto
    {
        string Id { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
    }
}
