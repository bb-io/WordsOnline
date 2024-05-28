using Apps.WordsOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.WordsOnline.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    private static IEnumerable<ConnectionProperty> ConnectionProperties => new[]
    {
        new ConnectionProperty(CredsNames.ApiKey)
        {
            DisplayName = "API key",
            Description = "Developer API key for WordsOnline API, f.e.: 123e4567-e89b-12d3-a456-426614174000",
            Sensitive = true
        },
        new ConnectionProperty(CredsNames.ProjectId)
        {
            DisplayName = "Project GUID",
            Description = "F.e.: 123e4567-e89b-12d3-a456-426614174000",
            Sensitive = false
        }
    };

    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = ConnectionProperties
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values) =>
        values.Select(x =>
                new AuthenticationCredentialsProvider(AuthenticationCredentialsRequestLocation.None, x.Key, x.Value))
            .ToList();
}