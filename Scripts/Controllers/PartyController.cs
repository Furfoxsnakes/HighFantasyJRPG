using Godot;
using System;
using System.Collections.Generic;

public class PartyController : Node
{
    [Export] public List<CharacterDetails> Party = new List<CharacterDetails>();
    [Export] public List<PartyMember> PartyMembers = new List<PartyMember>();
    [Export] private PackedScene _partyMemberScene;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (var memberDetails in Party)
        {
            var instance = _partyMemberScene.Instance() as PartyMember;
            instance.Init(memberDetails);
            instance.Name = memberDetails.Name;
            PartyMembers.Add(instance);
            AddChild(instance);
        }
    }
}
