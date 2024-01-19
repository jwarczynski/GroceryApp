namespace Warczynski.Zbaszyniak.GroceryApp.BLC;

public static class BLCContainer
{
    static BLCContainer()
    {
        string? libraryName = System.Configuration.ConfigurationManager.AppSettings["libraryName"];
        Console.WriteLine($"libraryName: {libraryName}");
        
        _blc = new BLC(libraryName);
    }

    private static readonly BLC _blc;

    public static BLC Instance => _blc;
}
