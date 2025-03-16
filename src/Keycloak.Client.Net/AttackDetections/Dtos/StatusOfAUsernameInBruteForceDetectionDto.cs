using Keycloak.Client.Net.AttackDetections.Dtos.Interface;
using System.Text.Json.Serialization;

namespace Keycloak.Client.Net.AttackDetections.Dtos
{
    public class StatusOfAUsernameInBruteForceDetectionDto : IStatusOfAUsernameInBruteForceDetectionDto
    {
        [JsonPropertyName("failedLoginNotBefore")]
        public int FailedLoginNotBefore { get; set; }

        [JsonPropertyName("numFailures")]
        public int NumFailures { get; set; }

        [JsonPropertyName("numTemporaryLockouts")]
        public int NumTemporaryLockouts { get; set; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        [JsonPropertyName("lastIPFailure")]
        public string LastIPFailure { get; set; }

        [JsonPropertyName("lastFailure")]
        public long LastFailure { get; set; }
    }
}
