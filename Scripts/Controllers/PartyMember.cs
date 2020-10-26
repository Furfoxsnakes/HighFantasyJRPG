using Godot;
using System;
using HighFantasyJRPG.Scripts.Exceptions;
using HighFantasyJRPG.Scripts.Exceptions.Modifiers;

public class PartyMember : Node
{
    public CharacterDetails CharacterDetails { get; private set; }

    public void Init(CharacterDetails details)
    {
        CharacterDetails = details;
        CharacterDetails[StatTypes.LVL] = LevelForExperience(CharacterDetails[StatTypes.EXP]);
    }
    
    public override void _EnterTree()
    {
        this.AddObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), CharacterDetails);
        this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), CharacterDetails);
    }

    public override void _ExitTree()
    {
        this.RemoveObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), CharacterDetails);
        this.RemoveObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), CharacterDetails);
    }
    
    private void OnExpWillChange(object sender, object args)
    {
        var vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(CharacterDetails.EXP, CharacterDetails.MAX_EXPERIENCE, int.MaxValue));
    }
        
    private void OnExpDidChange(object sender, object args)
    {
        // update the LVL stat when the character receives new EXP
        GD.Print(args);
        CharacterDetails.SetValue(StatTypes.LVL, LevelForExperience(CharacterDetails.EXP), false);
    }

    public static int ExperienceForLevel(int lvl)
    {
        var levelPercent = (float) (lvl - CharacterDetails.MIN_LEVEL) / (CharacterDetails.MAX_LEVEL - CharacterDetails.MIN_LEVEL);
        return (int)EasingEquations.EaseInQuad(0, CharacterDetails.MAX_EXPERIENCE, levelPercent);
    }

    public static int LevelForExperience(int exp)
    {
        var lvl = CharacterDetails.MAX_LEVEL;
        for (; lvl >= CharacterDetails.MIN_LEVEL; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }
}
