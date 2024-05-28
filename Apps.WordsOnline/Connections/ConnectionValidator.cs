using Apps.WordsOnline.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.WordsOnline.Connections;

public class ConnectionValidator : IConnectionValidator
{
    private readonly ApiClient _client = new();

    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.Execute("/projects/[projectGuid]", Method.Get, null, authenticationCredentialsProviders.ToList());
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