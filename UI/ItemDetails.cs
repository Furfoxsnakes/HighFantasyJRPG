using Godot;

public class ItemDetails : Node
{
    private RichTextLabel _itemDescription;
    private Label _itemName;

    public override void _Ready()
    {
        _itemName = GetNode<Label>("ItemName");
        _itemDescription = GetNode<RichTextLabel>("ItemDescription");
    }

    public void DisplayItemDetails(Item item)
    {
        _itemName.Text = item.Name;
        _itemDescription.Text = item.Description;
    }
}