using Godot;

public class InventoryUI : CanvasLayer
{
    private Control _container;
    private Item _selectedItem;
    public Game Game;

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