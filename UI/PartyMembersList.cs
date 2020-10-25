using Godot;

public class PartyMembersList : Node
{
    [Signal]
    public delegate void PartyMemberSelected(int id);

    private Game _game;

    [Export] private PackedScene _partyMemberPanelScene;

    public PartyMemberPanel this[int index] => GetChild<PartyMemberPanel>(index);
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<Game>("Game");
    }

    public void UpdatePartyMembers(int selectedPartyMemberId = 0)
    {
        Clear();
        var partyMembers = _game.PartyController.Party;

        if (partyMembers.Count == 0) return;
        
        for (var i = 0; i < partyMembers.Count; ++i)
        {
            var panelInstance = _partyMemberPanelScene.Instance() as PartyMemberPanel;
            panelInstance.Index = i;
            panelInstance.CharacterDetails = partyMembers[i];
            panelInstance.Connect("PartyMemberSelected", this, nameof(OnPartyMemberSelected));
            AddChild(panelInstance);
        }

        // out of bounds check
        if (selectedPartyMemberId >= GetChildCount())
            selectedPartyMemberId = 0;

        SelectPartyMember(selectedPartyMemberId);
    }

    public void SelectPartyMember(int memberId)
    {
        for (var i = 0; i < GetChildCount(); ++i) this[i].Pressed = i == memberId;
        EmitSignal(nameof(PartyMemberSelected), memberId);
    }

    private void OnPartyMemberSelected(int selectedId)
    {
        SelectPartyMember(selectedId);
    }

    public void Clear()
    {
        foreach (Node child in GetChildren()) child.QueueFree();
    }
}