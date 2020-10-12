using Godot;

public class HealthPotion : Consumable
{
    public override bool Activate(CharacterDetails character)
    {
        if (character.HP == character.MHP) return false;

        character.HP = Mathf.Min(character.HP + Amount, character.MHP);

        return true;
    }
}