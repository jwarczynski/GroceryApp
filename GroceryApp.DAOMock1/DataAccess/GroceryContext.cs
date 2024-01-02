using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock1.DataAccess;

public class GroceryContext: DbContext
{
    public string DbPath { get; }

    public GroceryContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "grocery.db");
    }
    
    public DbSet<Grocery> Groceries { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}