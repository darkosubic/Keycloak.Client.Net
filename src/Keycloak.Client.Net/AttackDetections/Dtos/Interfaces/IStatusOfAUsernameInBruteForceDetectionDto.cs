namespace Keycloak.Client.Net.AttackDetections.Dtos.Interfaces
{
    public interface IStatusOfAUsernameInBruteForceDetectionDto
    {
        bool Disabled { get; set; }

        int FailedLoginNotBefore { get; set; }

        long LastFailure { get; set; }

        string LastIPFailure { get; set; }

        int NumFailures { get; set; }

        int NumTemporaryLockouts { get; set; }
    }
}
