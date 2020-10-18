using System.Collections.Generic;
using Godot;
using HighFantasyJRPG.Scripts.Exceptions;

public class CharacterDetails : Resource
{
    [Export] public string Name;
    [Export] public Texture CharacterPortrait;
    [Export] public Texture CharacterFull;
    
    #region Stats

    public int this[StatTypes type]
    {
        get => _data[(int) type];
        set => SetValue(type, value, true);
    }

    [Export] private int[] _data = new int[(int) StatTypes.Count];
    
    public void SetValue(StatTypes type, int value, bool allowException)
    {
        var oldValue = this[type];
        if (value == oldValue) return;

        if (allowException)
        {
            var exc = new ValueChangeException(oldValue, value);

            this.PostNotification(WillChangeNotification(type), exc);
            
            value = Mathf.FloorToInt(exc.GetModifiedValue());

            if (exc.Toggle == false || value == oldValue) return;
        }

        _data[(int) type] = value;
        this.PostNotification(DidChangeNotification(type), oldValue);
    }

    #endregion

    #region Rank

    public const int MIN_LEVEL = 1;
    public const int MAX_LEVEL = 100;
    public const int MAX_EXPERIENCE = 9999999;

    public int LVL => this[StatTypes.LVL];

    public int EXP
    {
        get => this[StatTypes.EXP];
        set => this[StatTypes.EXP] = value;
    }

    public float LevelPercent => (float) (LVL - MIN_LEVEL) / (MAX_LEVEL - MIN_LEVEL);
    
    public static int ExperienceForLevel(int lvl)
    {
        var levelPercent = (float) (lvl - MIN_LEVEL) / (MAX_LEVEL - MIN_LEVEL);
        return (int)EasingEquations.EaseInQuad(0, MAX_EXPERIENCE, levelPercent);
    }

    public static int LevelForExperience(int exp)
    {
        var lvl = MAX_LEVEL;
        for (; lvl >= MIN_LEVEL; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }

    #endregion
    
    #region Notifications

    private static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    private static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, $"Stats.{type}WillChange");
        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, $"Stats.{type}DidChange");
        return _didChangeNotifications[type];
    }

    #endregion

}