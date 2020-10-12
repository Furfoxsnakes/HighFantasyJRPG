using Godot;
using System;

public class ManaPotion : Consumable
{
    public override bool Activate(CharacterDetails character)
    {
        if (character.MP == character.MMP) return false;

        character.MP = Mathf.Min(character.MP + Amount, character.MMP);

        return true;
    }
}
