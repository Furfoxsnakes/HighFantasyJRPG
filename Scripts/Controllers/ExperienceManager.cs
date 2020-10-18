using Godot;
using System;
using System.Collections.Generic;
using HighFantasyJRPG.Scripts.ViewModelComponents;

public static class ExperienceManager
{
    private const float _minLevelBonus = 1.5f;
    private const float _maxLevelBonus = 0.5f;

    public static void AwardExperience(int amount, List<PartyMember> party)
    {
        // grab a list of all the party member ranks
        var details = new List<CharacterDetails>(party.Count);
        foreach (var t in party)
        {
            details.Add(t.CharacterDetails);
        }
        
        // determine the range of the character's level
        var min = int.MaxValue;
        var max = int.MinValue;
        for (var i = details.Count - 1; i >= 0; --i)
        {
            min = Mathf.Min(details[i].LVL, min);
            max = Mathf.Max(details[i].LVL, max);
        }

        if (min == max) max++;
        
        // weigh the amount of exp to award to character based on their level
        var weights = new float[party.Count];
        var summedWeights = 0f;
        for (var i = details.Count - 1; i >= 0; --i)
        {
            var percent = (float) (details[i].LVL - min) / (float)(max - min);
            weights[i] = Mathf.Lerp(_minLevelBonus, _maxLevelBonus, percent);
            summedWeights += weights[i];
        }
        
        // hand out the weighted exp
        for (var i = details.Count - 1; i >= 0; --i)
        {
            var subAmount = Mathf.FloorToInt((weights[i] / summedWeights) * amount);
            details[i].EXP += subAmount;
        }
    }
}
