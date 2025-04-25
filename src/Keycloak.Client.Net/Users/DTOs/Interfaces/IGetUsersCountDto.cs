namespace Keycloak.Client.Net.Users.DTOs.Interfaces
{
    public interface IGetUsersCountDto
    {
        string Email { get; set; }
        bool? EmailVerified { get; set; }
        bool? Enabled { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Search { get; set; }
        string SearchQuery { get; set; }
        string Username { get; set; }
    }
}
