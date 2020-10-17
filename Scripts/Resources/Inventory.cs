using System.Collections.Generic;
using Godot;

public class Inventory : Resource
{
    [Export] public Dictionary<Item, int> Items;
}