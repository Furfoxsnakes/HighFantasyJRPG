using Godot;

public class PartyMemberPanel : Button
{
    [Signal]
    public delegate void PartyMemberSelected(int selectedId);

    public CharacterDetails CharacterDetails;
    [Export] public NodePath ExpLabelPath;
    [Export] public NodePath ExpProgressPath;
    [Export] public NodePath HpLabelPath;
    [Export] public NodePath HpProgressPath;

    public int Index;
    [Export] public NodePath LevelLabelPath;
    [Export] public NodePath MpLabelPath;
    [Export] public NodePath MpProgressPath;
    [Export] public NodePath NameLabelPath;
    [Export] public NodePath PortraitPath;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Init();
    }

    public override void _EnterTree()
    {
        this.AddObserver(OnHealthChanged, CharacterDetails.DidChangeNotification(StatTypes.HP), CharacterDetails);
        this.AddObserver(OnHealthChanged, CharacterDetails.DidChangeNotification(StatTypes.MHP), CharacterDetails);
        this.AddObserver(OnManaChanged, CharacterDetails.DidChangeNotification(StatTypes.MP), CharacterDetails);
        this.AddObserver(OnManaChanged, CharacterDetails.DidChangeNotification(StatTypes.MMP), CharacterDetails);
        this.AddObserver(OnExpChanged, CharacterDetails.DidChangeNotification(StatTypes.EXP), CharacterDetails);
        this.AddObserver(OnLvlChanged, CharacterDetails.DidChangeNotification(StatTypes.LVL), CharacterDetails);
    }

    public override void _ExitTree()
    {
        this.RemoveObserver(OnHealthChanged, CharacterDetails.DidChangeNotification(StatTypes.HP), CharacterDetails);
        this.RemoveObserver(OnHealthChanged, CharacterDetails.DidChangeNotification(StatTypes.MHP), CharacterDetails);
        this.RemoveObserver(OnManaChanged, CharacterDetails.DidChangeNotification(StatTypes.MP), CharacterDetails);
        this.RemoveObserver(OnManaChanged, CharacterDetails.DidChangeNotification(StatTypes.MMP), CharacterDetails);
        this.RemoveObserver(OnExpChanged, CharacterDetails.DidChangeNotification(StatTypes.EXP), CharacterDetails);
        this.RemoveObserver(OnLvlChanged, CharacterDetails.DidChangeNotification(StatTypes.LVL), CharacterDetails);
    }

    private void OnLvlChanged(object sender, object arg2)
    {
        var details = sender as CharacterDetails;
        GetNode<Label>(LevelLabelPath).Text = $"Lv.{details.LVL}";
        GD.Print(CharacterDetails.LVL);
    }

    private void OnExpChanged(object sender, object arg2)
    {
        var details = sender as CharacterDetails;
        GetNode<ProgressBar>(ExpProgressPath).Value = 0.5f;
        GetNode<Label>(ExpLabelPath).Text = $"{details.EXP}";
    }
    
    private void OnManaChanged(object sender, object args)
    {
        var details = sender as CharacterDetails;
        GetNode<ProgressBar>(HpProgressPath).Value = 0.5f;
        GetNode<Label>(HpLabelPath).Text = $"{details[StatTypes.MP]}/{details[StatTypes.MMP]}";
    }

    private void OnHealthChanged(object sender, object args)
    {
        var details = sender as CharacterDetails;
        GetNode<ProgressBar>(HpProgressPath).Value = details[StatTypes.HP] / details[StatTypes.MHP];
        GetNode<Label>(HpLabelPath).Text = $"{details[StatTypes.HP]}/{details[StatTypes.MHP]}";
    }

    public void Init()
    {
        // DETAILS
        GetNode<Label>(LevelLabelPath).Text = $"Lv.{CharacterDetails.LVL}";
        GetNode<Label>(NameLabelPath).Text = CharacterDetails.Name;
        GetNode<TextureRect>(PortraitPath).Texture = CharacterDetails.CharacterPortrait;

        // XP
        GetNode<ProgressBar>(ExpProgressPath).Value = (float)CharacterDetails.EXP / CharacterDetails.ExperienceForLevel(CharacterDetails.LVL + 1);
        GetNode<Label>(ExpLabelPath).Text = $"{CharacterDetails.EXP}/{CharacterDetails.ExperienceForLevel(CharacterDetails.LVL + 1)}";

        //HP
        GetNode<ProgressBar>(HpProgressPath).Value = (float)CharacterDetails[StatTypes.HP] / CharacterDetails[StatTypes.MHP];
        GetNode<Label>(HpLabelPath).Text = $"{CharacterDetails[StatTypes.HP]}/{CharacterDetails[StatTypes.MHP]}";

        // MANA
        if (CharacterDetails[StatTypes.MMP] > 0)
            GetNode<ProgressBar>(MpProgressPath).Value = (float)CharacterDetails[StatTypes.MP] / CharacterDetails[StatTypes.MMP];
        else
            GetNode<ProgressBar>(MpProgressPath).Value = 0;
        
        GetNode<Label>(MpLabelPath).Text = $"{CharacterDetails[StatTypes.MP]}/{CharacterDetails[StatTypes.MMP]}";

    }

    private void _on_PartyMemberPanel_pressed()
    {
        EmitSignal(nameof(PartyMemberSelected), Index);
    }
}