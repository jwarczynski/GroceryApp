﻿using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.SQLDao.BO;

namespace Warczynski.Zbaszyniak.GroceryApp.SQLDao.DataAccess;

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
    public DbSet<ApplicationUser> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}