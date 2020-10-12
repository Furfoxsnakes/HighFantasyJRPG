using Godot;
using System;
using System.Collections.Generic;

public class ItemDetails : Node
{
    private Label _itemName;
    private RichTextLabel _itemDescription;

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
