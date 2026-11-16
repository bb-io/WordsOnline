using Apps.WordsOnline.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.WordsOnline.Webhooks.Models.Requests;

public class RequestDeliveredRequest
{
    [Display("Request GUID", Description = "Optionally filters delivered events by request GUID"),
     DataSource(typeof(RequestDataHandler))]
    public string? RequestGuid { get; set; }

    [Display("Include source file")]
    public bool? IncludeSourceFile { get; set; }

    [Display("Target languages"), DataSource(typeof(TargetLanguageDataSource))]
    public IEnumerable<string>? TargetLanguages { get; set; }
}