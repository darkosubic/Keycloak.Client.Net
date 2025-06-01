namespace Keycloak.Client.Net.Users.DTOs.Interfaces
{
    public interface IGetUsersRequestDto
    {
        bool? BriefRepresentation { get; set; }
        string Email { get; set; }
        bool? IsEmailVerified { get; set; }
        bool? UserEnabled { get; set; }
        /// <summary>
        /// Flag which indicates, weather FirstName, LastName, Email or Username must match exactly. 
        /// </summary>
        bool? IsExact { get; set; }
        int? PaginationOffset { get; set; }
        string FirstName { get; set; }
        string IdpAlias { get; set; }
        string IdpUserId { get; set; }
        string LastName { get; set; }
        /// <summary>
        /// Default size is 100
        /// </summary>
        int? MaxResultCount { get; set; }
        string Q { get; set; }
        string Search { get; set; }
        string Username { get; set; }
    }
}
