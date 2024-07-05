using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.WordsOnline.DataSources.Static;

public class RequestStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            { "In progress", "In Progress" },
            { "Preparing Quote", "Preparing Quote" },
            { "Delivered", "Delivered" },
            { "Rejected", "Rejected" },
            { "Imported", "Imported" },
            { "Quote Submitted", "Quote Submitted" },
            { "Cancelled", "Cancelled" }
        };
    }
}