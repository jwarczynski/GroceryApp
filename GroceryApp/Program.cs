// See https://aka.ms/new-console-template for more information

using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp_Architecture;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        string? libraryName = System.Configuration.ConfigurationManager.AppSettings["libraryName"];
        Console.WriteLine($"libraryName: {libraryName}");
        
        BLC blc = new BLC(libraryName);

        foreach (IProduct product in blc.GetAllProducts())
        {
            Console.WriteLine($"{product.Name}: {product.Price}");
        }
        Console.WriteLine("----------------------");
        foreach (IGrocery grocery in blc.GetAllGroceries())
        {
            Console.WriteLine($"{grocery.Name}: {grocery.Address}");
        }
    }
}    


