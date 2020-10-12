using Godot;
using System;

public class CharactersTab : Tabs
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    private Game _game;
    private PartyMembersList _partyMembersList;
    private PartyMemberDetails _partyMemberDetails;
    
    [Export]
    private PackedScene _partyMemberPanelScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<Game>("Game");
        _partyMembersList = GetNode<PartyMembersList>("HBoxContainer/VBoxContainer/PartyMembersList");
        _partyMemberDetails = GetNode<PartyMemberDetails>("HBoxContainer/PartyMemberDetails");

        _partyMembersList.Connect("PartyMemberSelected", this, nameof(OnPartyMemberSelected));
        _partyMembersList.UpdatePartyMembers();
    }

    private void OnPartyMemberSelected(int selectedId)
    {
        var partyMemberDetails = _partyMembersList[selectedId].CharacterDetails;
        _partyMemberDetails.DisplayCharacterDetails(partyMemberDetails);
    }
}
