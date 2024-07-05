using Apps.WordsOnline.DataSources.Static;
using Apps.WordsOnline.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.Models.Requests;

public class ApproveQuoteRequest : RequestIdentifier
{
    [StaticDataSource(typeof(QuoteStatusDataSource))]
    public string? Action { get; set; }
    
    public string? Description { get; set; }
}