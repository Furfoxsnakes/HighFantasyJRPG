using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryUI : CanvasLayer
{
    public Game Game;
    private Control _container;
    private Item _selectedItem;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Game = GetTree().Root.GetNode<Game>("Game");
        _container = GetNode<Control>("Container");
    }

    public bool Toggle()
    {
        _container.Visible = !_container.Visible;
        return _container.Visible;
    }
}
