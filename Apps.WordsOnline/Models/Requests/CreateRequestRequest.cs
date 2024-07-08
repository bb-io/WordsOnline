using Apps.WordsOnline.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.WordsOnline.Models.Requests;

public class CreateRequestRequest
{
    [Display("Request name")]
    public string RequestName { get; set; } = string.Empty;
    
    [Display("Source language"), DataSource(typeof(SourceLanguageDataSource))]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target languages"), DataSource(typeof(TargetLanguageDataSource))]
    public IEnumerable<string> TargetLanguages { get; set; } = new List<string>();

    [Display("Content type"), DataSource(typeof(ContentTypeDataSource))]
    public string ContentType { get; set; } = string.Empty;
    
    [Display("Service level"), DataSource(typeof(ServiceLevelDataSource))]
    public string ServiceLevel { get; set; } = string.Empty;

    [Display("Is auto approve", Description = "By default, the request is auto-approved")]
    public bool? IsAutoApprove { get; set; }

    [Display("Client request ID")]
    public string? ClientRequestId { get; set; }
    
    [Display("Description")]
    public string? Description { get; set; } = string.Empty;
}