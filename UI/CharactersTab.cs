using Godot;

public class CharactersTab : Tabs
{
    private Game _game;
    private PartyMemberDetails _partyMemberDetails;

    [Export] private PackedScene _partyMemberPanelScene;

    private PartyMembersList _partyMembersList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<Game>("Game");
        _partyMembersList = GetNode<PartyMembersList>("HBoxContainer/VBoxContainer/PartyMembersList");
        _partyMemberDetails = GetNode<PartyMemberDetails>("HBoxContainer/PartyMemberDetails");

        _partyMembersList.Connect("PartyMemberSelected", this, nameof(OnPartyMemberSelected));
        //_partyMembersList.UpdatePartyMembers();
    }

    private void OnPartyMemberSelected(int selectedId)
    {
        var partyMemberDetails = _partyMembersList[selectedId].CharacterDetails;
        _partyMemberDetails.DisplayCharacterDetails(partyMemberDetails);
    }

    private void _on_Characters_visibility_changed()
    {
        if (Visible == true)
            _partyMembersList.UpdatePartyMembers();
    }
}