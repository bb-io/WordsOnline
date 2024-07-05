using Apps.WordsOnline.DataSources.Static;
using Apps.WordsOnline.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.Models.Requests;

public class UpdateRequestStatusRequest : RequestIdentifier
{
    [StaticDataSource(typeof(RequestActionDataSource))]
    public string Action { get; set; } = string.Empty;
    
    public string? Description { get; set; } = string.Empty;
}