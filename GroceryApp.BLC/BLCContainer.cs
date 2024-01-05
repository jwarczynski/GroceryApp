namespace Warczynski.Zbaszyniak.GroceryApp.BLC;

public static class BLCContainer
{
    private static readonly BLC _blc = new BLC("GroceryApp.DAOMock1.dll");

    public static BLC Instance => _blc;
}
