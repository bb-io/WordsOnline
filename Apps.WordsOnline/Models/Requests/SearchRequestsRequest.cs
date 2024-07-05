using Apps.WordsOnline.DataSources.Static;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.Models.Requests;

public class SearchRequestsRequest
{
    [StaticDataSource(typeof(RequestStatusDataSource))]
    public string? Status { get; set; }
}