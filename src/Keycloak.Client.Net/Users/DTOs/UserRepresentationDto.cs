using System.Collections.Generic;
using System.Text.Json.Serialization;
using Keycloak.Client.Net.Users.Inerfaces;

namespace Keycloak.Client.Net.Users.DTOs
{
    public class UserRepresentationDto : IUserRepresentationDto
    {
        public UserRepresentationDto()
        {
            Attributes = new Dictionary<string, List<string>>();
            UserProfileMetadata = new UserProfileMetadataDto();
            Credentials = new List<CredentialRepresentationDto>();
            DisableableCredentialTypes = new HashSet<string>();
            FederatedIdentities = new List<FederatedIdentityRepresentationDto>();
            ClientRoles = new Dictionary<string, List<string>>();
            ClientConsents = new List<UserConsentRepresentationDto>();
            ApplicationRoles = new Dictionary<string, List<string>>();
            SocialLinks = new List<SocialLinkRepresentationDto>();
            Groups = new List<string>();
            Access = new Dictionary<string, bool>();
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("emailVerified")]
        public bool? EmailVerified { get; set; }

        [JsonPropertyName("attributes")]
        public Dictionary<string, List<string>> Attributes { get; set; }

        [JsonPropertyName("userProfileMetadata")]
        public UserProfileMetadataDto UserProfileMetadata { get; set; }

        [JsonPropertyName("self")]
        public string Self { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("createdTimestamp")]
        public long? CreatedTimestamp { get; set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("totp")]
        public bool? Totp { get; set; }

        [JsonPropertyName("federationLink")]
        public string FederationLink { get; set; }

        [JsonPropertyName("serviceAccountClientId")]
        public string ServiceAccountClientId { get; set; }

        [JsonPropertyName("credentials")]
        public List<CredentialRepresentationDto> Credentials { get; set; }

        [JsonPropertyName("disableableCredentialTypes")]
        public HashSet<string> DisableableCredentialTypes { get; set; }

        [JsonPropertyName("requiredActions")]
        public List<string> RequiredActions { get; set; } = new List<string>();

        [JsonPropertyName("federatedIdentities")]
        public List<FederatedIdentityRepresentationDto> FederatedIdentities { get; set; }

        [JsonPropertyName("realmRoles")]
        public List<string> RealmRoles { get; set; } = new List<string>();

        [JsonPropertyName("clientRoles")]
        public Dictionary<string, List<string>> ClientRoles { get; set; }

        [JsonPropertyName("clientConsents")]
        public List<UserConsentRepresentationDto> ClientConsents { get; set; }

        [JsonPropertyName("notBefore")]
        public int? NotBefore { get; set; }

        [JsonPropertyName("applicationRoles")]
        public Dictionary<string, List<string>> ApplicationRoles { get; set; }

        [JsonPropertyName("socialLinks")]
        public List<SocialLinkRepresentationDto> SocialLinks { get; set; }

        [JsonPropertyName("groups")]
        public List<string> Groups { get; set; }

        [JsonPropertyName("access")]
        public Dictionary<string, bool> Access { get; set; }
    }
}
