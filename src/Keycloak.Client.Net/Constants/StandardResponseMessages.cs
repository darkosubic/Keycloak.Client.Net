using System.Net;

namespace Keycloak.Client.Net.Constants
{
    internal static class StandardResponseMessages
    {
        internal const string UnauthorizedMessage = "Unauthorized. Make sure your request includes a valid token.";
        internal const string RequestFailedMessageStart = "Failed to get an OK status. Response message:";
        private const string HttpCodeMessage = ", HttpCode: ";
        private const string ForbiddenMessageStart = "Forbidden. Make sure your token includes ";
        private const string ForbiddenMessageEnd = " roles.";

        public static string ForbiddenMessage(string roles)
        {
            return ForbiddenMessageStart + roles + ForbiddenMessageEnd;
        }

        public static string RequestFailedMessage(HttpStatusCode code, string message)
        {
            return RequestFailedMessageStart + message + HttpCodeMessage + code;
        }
    }
}
