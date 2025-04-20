using System.Collections.Generic;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Inerfaces
{
    public interface IUserRepresentationDto
    {
        Dictionary<string, bool> Access { get; set; }
        Dictionary<string, List<string>> ApplicationRoles { get; set; }
        Dictionary<string, List<string>> Attributes { get; set; }
        List<UserConsentRepresentationDto> ClientConsents { get; set; }
        Dictionary<string, List<string>> ClientRoles { get; set; }
        long? CreatedTimestamp { get; set; }
        List<CredentialRepresentationDto> Credentials { get; set; }
        HashSet<string> DisableableCredentialTypes { get; set; }
        string Email { get; set; }
        bool? EmailVerified { get; set; }
        bool? Enabled { get; set; }
        List<FederatedIdentityRepresentationDto> FederatedIdentities { get; set; }
        string FederationLink { get; set; }
        string FirstName { get; set; }
        List<string> Groups { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        int? NotBefore { get; set; }
        string Origin { get; set; }
        List<string> RealmRoles { get; set; }
        List<string> RequiredActions { get; set; }
        string Self { get; set; }
        string ServiceAccountClientId { get; set; }
        List<SocialLinkRepresentationDto> SocialLinks { get; set; }
        bool? Totp { get; set; }
        string Username { get; set; }
        UserProfileMetadataDto UserProfileMetadata { get; set; }
    }
}
