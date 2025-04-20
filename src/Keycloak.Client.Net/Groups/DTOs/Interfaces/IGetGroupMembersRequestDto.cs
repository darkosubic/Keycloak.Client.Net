namespace Keycloak.Client.Net.Groups.DTOs.Interfaces
{
    public interface IGetGroupMembersRequestDto
    {
        bool? BriefRepresentation { get; set; }
        int? FirstResult { get; set; }
        int? MaxResults { get; set; }
    }
}
