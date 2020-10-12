using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Game : Node
{
    public PlayerCharacter Player;
    public InventoryUI InventoryUI;

    [Export] public List<CharacterDetails> PartyMembers;
    [Export] public Inventory Inventory;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = GetNode<PlayerCharacter>("Characters/PlayerCharacter");
        InventoryUI = GetNode<InventoryUI>("InventoryUI");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key)
        {
            if (Input.IsActionJustPressed("ToggleInventory"))
                ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        Player.CanMove = !InventoryUI.Toggle();
    }
}
