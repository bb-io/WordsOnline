using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.DataSources.Static;

public class QuoteStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "Approve", "Approve" },
            { "Cancel", "Cancel" }
        };
    }
}