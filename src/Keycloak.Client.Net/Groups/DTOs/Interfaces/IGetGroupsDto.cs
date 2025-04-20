namespace Keycloak.Client.Net.Groups.DTOs.Interfaces
{
    public interface IGetGroupsDto
    {
        bool BriefRepresentation { get; set; }
        bool? SearchByGroupNameByExactValue { get; set; }
        int? FirstResult { get; set; }
        int? MaxResults { get; set; }
        bool PopulateHierarchy { get; set; }
        string SearchByGroupName { get; set; }
        string SearchByAtributeKeyAndValue { get; set; }
    }
}
