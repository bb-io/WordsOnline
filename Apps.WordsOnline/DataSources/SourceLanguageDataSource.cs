using Apps.WordsOnline.Constants;
using Apps.WordsOnline.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.WordsOnline.DataSources;

public class SourceLanguageDataSource(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var actions = new Actions.Actions(InvocationContext, null!);
        
        string projectGuid = Creds.Get(CredsNames.ProjectGuid).Value;
        var project = await actions.GetProject(projectGuid);

        return project.LanguagePairs
            .Select(x => new { x.SourceLanguageCode, x.SourceLanguageName })
            .DistinctBy(x => x.SourceLanguageCode)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.SourceLanguageName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.SourceLanguageCode, x => x.SourceLanguageName);
    }
}