using Godot;
using HighFantasyJRPG.Scripts.Resources;

public class DialogOptions : ItemList
{
    public void ShowOptions(NpcDialogData data)
    {
        Clear();
        Visible = true;

        foreach (var option in data.Dialog) AddItem(option.Name);
    }
}