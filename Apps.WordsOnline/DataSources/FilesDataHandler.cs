using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.WordsOnline.DataSources;

public class FilesDataHandler(InvocationContext invocationContext, [ActionParameter] DownloadFilesRequest request) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.RequestId))
        {
            throw new Exception("You should input a request id first.");
        }
        
        var actions = new Actions.Actions(InvocationContext, null);
        var requestResponse = await actions.GetFilesFromRequest(request.RequestId);

        return requestResponse
            .Where(x => string.IsNullOrEmpty(context.SearchString) || GetReadableName(x).Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Guid, GetReadableName);
    }
    
    private string GetReadableName(FileInfoDto file)
    {
        return $"({file.Type}) {file.Name}";
    }
}