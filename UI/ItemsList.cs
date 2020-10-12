using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ItemsList : ItemList
{
    private Game _game;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<Game>("Game");
    }

    public void PopulateItems(int selectedId = 0)
    {
        Clear();

        if (_game.Inventory.Items.Count <= 0) return;
        
        KeyValuePair<Item, int> item;
        for (var i = 0; i < _game.Inventory.Items.Count; ++i)
        {
            item = _game.Inventory.Items.ElementAt(i);
            AddItem($"{item.Key.Name} ({item.Value})", item.Key.Icon);
        }

        Select(selectedId);
        EmitSignal("item_selected", selectedId);
    }
}
