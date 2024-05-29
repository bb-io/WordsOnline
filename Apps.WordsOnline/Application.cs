using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.WordsOnline;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => 
        [
            ApplicationCategory.LspPortal
        ];
        set { }
    }
    
    public string Name
    {
        get => "WordsOnline";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}