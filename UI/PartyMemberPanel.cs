using Godot;
using System;

public class PartyMemberPanel : Button
{
    [Export] public NodePath LevelLabelPath;
    [Export] public NodePath NameLabelPath;
    [Export] public NodePath ExpProgressPath;
    [Export] public NodePath ExpLabelPath;
    [Export] public NodePath HpProgressPath;
    [Export] public NodePath HpLabelPath;
    [Export] public NodePath MpProgressPath;
    [Export] public NodePath MpLabelPath;
    [Export] public NodePath PortraitPath;

    public int Index;
    public CharacterDetails CharacterDetails;

    [Signal]
    public delegate void PartyMemberSelected(int selectedId);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Init();
    }

    public void Init()
    {
        GetNode<Label>(LevelLabelPath).Text = $"Lv.{CharacterDetails.LVL}";
        GetNode<Label>(NameLabelPath).Text = CharacterDetails.Name;
        GetNode<ProgressBar>(ExpProgressPath).Value = 0.5f;
        GetNode<Label>(ExpLabelPath).Text = $"{CharacterDetails.EXP}";
        GetNode<ProgressBar>(HpProgressPath).Value = 0.5f;
        GetNode<Label>(HpLabelPath).Text = $"{CharacterDetails.HP}/{CharacterDetails.MHP}";
        GetNode<ProgressBar>(MpProgressPath).Value = 0.5f;
        GetNode<Label>(MpLabelPath).Text = $"{CharacterDetails.MP}/{CharacterDetails.MMP}";
        GetNode<TextureRect>(PortraitPath).Texture = CharacterDetails.CharacterPortrait;
    }

    private void _on_PartyMemberPanel_pressed()
    {
        EmitSignal(nameof(PartyMemberSelected), Index);
    }
}
