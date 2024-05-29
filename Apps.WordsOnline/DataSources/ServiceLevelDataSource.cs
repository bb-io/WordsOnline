using Apps.WordsOnline.Constants;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.WordsOnline.DataSources;

public class ServiceLevelDataSource(InvocationContext invocationContext, [ActionParameter] CreateRequestRequest request) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var actions = new Actions.Actions(InvocationContext, null);
        
        string projectGuid = Creds.Get(CredsNames.ProjectGuid).Value;
        var project = await actions.GetProject(projectGuid);

        if (string.IsNullOrEmpty(request.ContentType))
        {
            throw new Exception($"You should input a content type first.");
        }

        return project.ServiceLevels
            .Where(x => x.ContentType == request.ContentType)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Name, x => x.Name);
    }
}