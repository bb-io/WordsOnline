using Apps.WordsOnline.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.WordsOnline.Models.Identifiers;

public class RequestIdentifier
{
    [Display("Request GUID"), DataSource(typeof(RequestDataHandler))]
    public string RequestId { get; set; } = string.Empty;
}