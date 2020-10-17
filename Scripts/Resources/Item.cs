using Godot;

public class Item : Resource
{
    [Export] public string Description;
    [Export] public Texture Icon;
    [Export] public string Name;
    [Export] public int PurchaseAmount;
    [Export] public int StackSize = 1;

    public virtual bool Activate(CharacterDetails character)
    {
        return true;
    }
}