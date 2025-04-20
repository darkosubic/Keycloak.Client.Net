using Keycloak.Client.Net.Groups.DTOs.Interfaces;

namespace Keycloak.Client.Net.Groups.DTOs
{
    public class GetGroupsDto : IGetGroupsDto
    {
        public GetGroupsDto()
        {
            SearchByGroupNameByExactValue = false;
            BriefRepresentation = true;
            PopulateHierarchy = true;
        }

        /// <summary>
        /// Use full or partial group name to find it
        /// </summary>
        public string SearchByGroupName { get; set; }

        /// <summary>
        /// Filters results by atribute name and value
        /// Format string in format "hello:world foo:bar"
        /// </summary>
        public string SearchByAtributeKeyAndValue { get; set; }

        /// <summary>
        /// Default value is false.
        /// If set to true, SearchByGroupName will search for the exact
        /// name of the group. If set to false, you can retrive multiple
        /// groups as long as part of their name contains value in SearchByGroupName
        /// </summary>
        public bool? SearchByGroupNameByExactValue { get; set; }

        public int? FirstResult { get; set; }

        public int? MaxResults { get; set; }

        /// <summary>
        /// Default valus is true.
        /// If set to true, Attributes, RealmRoles and ClientRoles are excluded from the response.
        /// </summary>
        public bool BriefRepresentation { get; set; }

        /// <summary>
        /// Default value is true.
        /// If set to true, response will retrieve nested sub-groups.
        /// </summary>
        public bool PopulateHierarchy { get; set; }
    }
}
