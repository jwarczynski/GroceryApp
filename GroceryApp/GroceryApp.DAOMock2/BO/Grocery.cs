﻿using GroceryApp.Interfaces;

namespace GroceryApp.DAOMock2.BO;

public class Grocery: IGrocery
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}