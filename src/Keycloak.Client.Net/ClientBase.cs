using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ardalis.Result;
using RestSharp;
using static Keycloak.Client.Net.Constants.StandardResponseMessages;

namespace Keycloak.Client.Net
{
    public abstract class ClientBase
    {
        internal Result HandleStandardErrorResponses(string forbiddenMessage, RestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Result.Forbidden(ForbiddenMessage(forbiddenMessage));
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Result.Unauthorized(UnauthorizedMessage);
            }

            return Result.Error(RequestFailedMessage(response.StatusCode, response.Content));
        }
    }
}
