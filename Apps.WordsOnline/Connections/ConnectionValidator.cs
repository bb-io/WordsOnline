using Apps.WordsOnline.Invocables;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.WordsOnline.Connections;

public class ConnectionValidator(InvocationContext invocationContext) : AppInvocable(invocationContext), IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await Client.Execute("/projects/[projectGuid]", Method.Get, null, Creds.ToList());
            return new ConnectionValidationResponse
            {
                IsValid = response.IsSuccessful,
                Message = response.Content
            };
        }
        catch (Exception e)
        {
            return new ConnectionValidationResponse
            {
                IsValid = false,
                Message = e.Message
            };
        }
    }
}