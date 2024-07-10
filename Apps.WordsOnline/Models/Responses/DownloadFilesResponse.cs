using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.WordsOnline.Models.Responses;

public class DownloadFilesResponse
{
    public List<FileObject> Files { get; set; } = new();
}

public class FileObject
{
    public FileReference File { get; set; } = new();

    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language")]
    public string TargetLanguage { get; set; } = string.Empty;
    
    [Display("File name")]
    public string FileName { get; set; } = string.Empty;
}