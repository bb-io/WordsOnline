using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.WordsOnline.Models.Responses;

public class DownloadFilesResponse
{
    public List<FileReference> Files { get; set; } = new();
}