using Godot;

public class PartyMemberDetails : Node
{
    private Label _dexterityLabel;
    private Label _energyLabel;
    private TextureRect _fullPortrait;
    private Label _lifeLabel;
    private Label _manaLabel;
    private Label _staminaLabel;
    private Label _strengthLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _fullPortrait = Owner.GetNode<TextureRect>("Container/Tabs/Characters/Full Portrait");
        _lifeLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Life/Value");
        _manaLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Mana/Value");
        _strengthLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Strength/Value");
        _dexterityLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Dexterity/Value");
        _energyLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Energy/Value");
        _staminaLabel = GetNode<Label>("MarginContainer/PartyMemberStats/Stamina/Value");
    }

    public void DisplayCharacterDetails(CharacterDetails details)
    {
        
    }
}