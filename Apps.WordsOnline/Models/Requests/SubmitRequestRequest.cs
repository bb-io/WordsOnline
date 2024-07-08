using Apps.WordsOnline.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.WordsOnline.Models.Requests;

public class SubmitRequestRequest : RequestIdentifier
{
    [Display("Notify file uploaded")]
    public bool? NotifyFileUploaded { get; set; }
}