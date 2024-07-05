using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.DataSources.Static;

public class RequestActionDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "Imported", "Imported" },
            { "ImportFailed", "ImportFailed" },
            { "Finished", "Finished" },
            { "Rejected", "Rejected" }
        };
    }
}