// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.DataAccess;
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
        Console.WriteLine("----------------------");
        
        using (var db = new GroceryContext())
        {
            var grocery = new Grocery() { Name = "Lidl", Address = "ul. Wrocławska 1" };
            db.Groceries.Add(grocery);
            db.SaveChanges();

            var product1 = new Product() { Name = "Apple", Price = 0.5f, GroceryEntity = grocery };
            var product2 = new Product() { Name = "Banana", Price = 0.3f, GroceryEntity = grocery };
            db.Products.AddRange(new[] { product1, product2 });
            db.SaveChanges();

            // Retrieve and print the added grocery and its products
            var retrievedGrocery = db.Groceries.Include(g => g.Products).First(g => g.Id == grocery.Id);
            Console.WriteLine($"Grocery: {retrievedGrocery.Name}, Address: {retrievedGrocery.Address}");
            foreach (var product in retrievedGrocery.Products)
            {
                Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
            }
        }


    }
}    


