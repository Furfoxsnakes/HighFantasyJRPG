using System.Collections.Generic;
using Godot;

public class PlayerCharacter : OverworldCharacter
{
    [Export] public bool CanMove = true;
    [Export] public Dictionary<Item, int> InventoryDict = new Dictionary<Item, int>();
    [Export] public List<Item> InventoryList = new List<Item>();

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key)
            if (key.Pressed && key.Echo != true)
                if (key.Scancode == OS.FindScancodeFromString("E"))
                    if (CollidedCharacter != null)
                        if (CollidedCharacter is NPCCharacter npc)
                            DialogController.Instance.DisplayDialogOptions(npc.DialogDataFilename);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (!CanMove) return;

        // WASD movement
        //var movement = new Vector2();
        Velocity.x = Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft");
        Velocity.y = Input.GetActionStrength("MoveDown") - Input.GetActionStrength("MoveUp");

        if (Velocity != Vector2.Zero) Direction = Velocity;
    }

    public override void _Ready()
    {
        base._Ready();

        var healthPotion = ResourceLoader.Load<Item>("res://Resources/HealthPotion.tres");
        AddItemToInventory(healthPotion);
        
    }

    /// <summary>
    ///     Adds an <see cref="Item" /> to the player's inventory
    /// </summary>
    /// <param name="item">Item to be assed</param>
    /// <returns>True if the item was successfully added</returns>
    public bool AddItemToInventory(Item item)
    {
        if (InventoryDict.ContainsKey(item))
        {
            if (InventoryDict[item] == item.StackSize)
            {
                GD.Print($"Already at max stack for {item.Name}.");
                return false;
            }

            InventoryDict[item]++;
        }
        else
        {
            InventoryDict.Add(item, 1);
        }

        return true;
    }
}