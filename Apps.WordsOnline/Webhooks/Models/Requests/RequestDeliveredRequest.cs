using Apps.WordsOnline.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.WordsOnline.Webhooks.Models.Requests;

public class RequestDeliveredRequest
{
    [Display("Include source file")]
    public bool? IncludeSourceFile { get; set; }

    [Display("Target languages"), DataSource(typeof(TargetLanguageDataSource))]
    public IEnumerable<string>? TargetLanguages { get; set; }
}