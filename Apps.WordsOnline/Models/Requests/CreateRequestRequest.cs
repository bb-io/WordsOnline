using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.WordsOnline.Models.Requests;

public class CreateRequestRequest
{
    [Display("Request name")]
    public string RequestName { get; set; } = string.Empty;
    
    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target languages")]
    public IEnumerable<string> TargetLanguages { get; set; } = new List<string>();

    [Display("Content type")]
    public string ContentType { get; set; } = string.Empty;
    
    [Display("Service level")]
    public string ServiceLevel { get; set; } = string.Empty;

    [Display("Source files")]
    public IEnumerable<FileReference> SourceFiles { get; set; } = new List<FileReference>();
    
    [Display("Reference files")]
    public IEnumerable<FileReference>? ReferenceFiles { get; set; }

    [Display("Is auto approve", Description = "By default, the request is auto-approved")]
    public bool? IsAutoApprove { get; set; }

    [Display("Client request ID")]
    public string? ClientRequestId { get; set; }
    
    [Display("Description")]
    public string? Description { get; set; } = string.Empty;
}