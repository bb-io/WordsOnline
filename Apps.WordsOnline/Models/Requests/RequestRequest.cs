using Blackbird.Applications.Sdk.Common;

namespace Apps.WordsOnline.Models.Requests;

public class GetRequestRequest
{
    [Display("Request ID")]
    public string RequestId { get; set; } = string.Empty;
}