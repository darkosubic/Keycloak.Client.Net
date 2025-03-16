namespace Keycloak.Client.Net.Builders
{
    public class RealmSettingsBuilder
    {
        private string _realmName = string.Empty;
        private string _keycloakUrl = string.Empty;

        public static RealmSettingsBuilder New() => new RealmSettingsBuilder();

        public RealmSettingsBuilder RealmName(string realmName)
        {
            _realmName = realmName;

            return this;
        }

        public RealmSettingsBuilder ServerUrl(string keycloakUrl)
        {
            _keycloakUrl = keycloakUrl;

            return this;
        }

        public RealmSettings Build()
        {
            return new RealmSettings()
            {
                Name = _realmName,
                Url = _keycloakUrl
            };
        }
    }
}
