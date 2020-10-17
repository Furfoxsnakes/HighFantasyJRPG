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
        _lifeLabel.Text = $"{details.HP} / {details.MHP}";
        _manaLabel.Text = $"{details.MP} / {details.MMP}";
        _strengthLabel.Text = details.STR.ToString();
        _dexterityLabel.Text = details.DEX.ToString();
        _energyLabel.Text = details.NRG.ToString();
        _staminaLabel.Text = details.STA.ToString();
        _fullPortrait.Texture = details.CharacterFull;
    }
}