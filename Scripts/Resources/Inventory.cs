using Godot;
using System;
using System.Collections.Generic;

public class Inventory : Resource
{
    [Export] public Dictionary<Item, int> Items;
}
