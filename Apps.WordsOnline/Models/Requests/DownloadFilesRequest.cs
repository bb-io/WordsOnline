﻿using Apps.WordsOnline.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.WordsOnline.Models.Requests;

public class DownloadFilesRequest
{
    [Display("Request ID"), DataSource(typeof(RequestDataHandler))]
    public string RequestId { get; set; } = string.Empty;
    
    [Display("Files"), DataSource(typeof(FilesDataHandler))]
    public IEnumerable<string>? Files { get; set; }
}