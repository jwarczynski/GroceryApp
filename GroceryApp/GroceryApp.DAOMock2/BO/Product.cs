﻿using GroceryApp.Core;
using GroceryApp.Interfaces;

namespace GroceryApp.DAOMock2.BO;

public class Product: IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public ProductCategory Category { get; set; }
    public float Magnesium { get; set; }
    public float Potassium { get; set; }
    public float Sodium { get; set; }
    public IGrocery Grocery { get; set; }
}