using Godot;

public class CharacterDetails : Resource
{
    #region Stats

    [Export] public string Name;
    [Export] public Texture CharacterPortrait;
    [Export] public Texture CharacterFull;

    [Export] public int LVL;
    [Export] public int EXP;
    [Export] public int MHP;

    [Export]
    public int HP
    {
        get => _HP;
        set
        {
            if (_HP == value) return;
            _HP = value;
            EmitSignal(nameof(HPChanged), _HP);
        }
    }

    private int _HP;

    [Export] public int MMP;

    [Export]
    public int MP
    {
        get => _MP;
        set
        {
            if (_MP == value) return;
            _MP = value;
            EmitSignal(nameof(MPChanged), _MP);
        }
    }

    private int _MP;
    [Export] public int STR;
    [Export] public int DEX;
    [Export] public int NRG;
    [Export] public int STA;

    #endregion

    #region Notifications

    [Signal]
    public delegate void HPChanged(int newValue);

    [Signal]
    public delegate void MPChanged(int newValue);

    #endregion
}