using Apps.WordsOnline.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.WordsOnline.Models.Requests;

public class UploadFileRequest : RequestIdentifier
{
    public FileReference File { get; set; }

    [Display("Reference files")]
    public IEnumerable<FileReference>? ReferenceFiles { get; set; }
}