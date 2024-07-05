using Apps.WordsOnline.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.Models.Requests;

public class SearchRequestsRequest
{
    [StaticDataSource(typeof(RequestStatusDataSource))]
    public string? Status { get; set; }
    
    [Display("Request name")]
    public string? RequestName { get; set; }
    
    [StaticDataSource(typeof(RequestStateDataSource))]
    public string? State { get; set; }

    [Display("Client request ID")]
    public string? ClientRequestId { get; set; }
}