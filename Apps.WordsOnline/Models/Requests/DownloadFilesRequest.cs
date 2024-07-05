using Apps.WordsOnline.DataSources;
using Apps.WordsOnline.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.WordsOnline.Models.Requests;

public class DownloadFilesRequest : RequestIdentifier
{
    [Display("Files"), DataSource(typeof(FilesDataHandler))]
    public IEnumerable<string>? Files { get; set; }

    [Display("Download all files")]
    public bool? DownloadAllFiles { get; set; }
}