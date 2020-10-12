using Godot;
using System;

public class Item : Resource
{
    [Export] public Texture Icon;
    [Export] public string Name;
    [Export] public int PurchaseAmount;
    [Export] public int StackSize = 1;
    [Export] public string Description;

    public virtual bool Activate(CharacterDetails character)
    {
        return true;
    }
}
