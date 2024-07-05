using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.DataSources.Static;

public class RequestStateDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>()
        {
            { "QuoteApproved", "Quote approved" },
            { "AutomationFailed", "Automation failed" },
            { "Created", "Created" },
            { "QuoteSubmitted", "Quote submitted" }
        };
    }
}