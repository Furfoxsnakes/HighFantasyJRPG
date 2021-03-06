using System.Collections.Generic;
using Godot;
using HighFantasyJRPG.Scripts.Exceptions;
using HighFantasyJRPG.Scripts.Exceptions.Modifiers;

public class Game : Node
{
    [Export] public Inventory Inventory;
    public InventoryUI InventoryUI;
    
    public PartyController PartyController;
    public PlayerCharacter Player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = GetNode<PlayerCharacter>("Characters/PlayerCharacter");
        InventoryUI = GetNode<InventoryUI>("InventoryUI");
        PartyController = GetNode<PartyController>("Party");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key)
            if (Input.IsActionJustPressed("ToggleInventory"))
                ToggleInventory();
    }

    public void ToggleInventory()
    {
        Player.CanMove = !InventoryUI.Toggle();
    }
}