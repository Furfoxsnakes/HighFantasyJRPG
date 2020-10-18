using Godot;
using HighFantasyJRPG.Scripts.Exceptions;
using HighFantasyJRPG.Scripts.Exceptions.Modifiers;

namespace HighFantasyJRPG.Scripts.ViewModelComponents
{
    public class Rank : Node
    {
        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;
        public const int MAX_EXPERIENCE = 9999999;

        public int LVL => _details[StatTypes.LVL];

        public float LevelPercent => (float) (LVL - MIN_LEVEL) / (MAX_LEVEL - MIN_LEVEL);

        public int EXP
        {
            get => _details[StatTypes.EXP];
            set => _details[StatTypes.EXP] = value;
        }
        
        private CharacterDetails _details;

        public override void _Ready()
        {
            _details = (Owner as Character).CharacterDetails;
        }

        public override void _EnterTree()
        {
            
        }

        public override void _ExitTree()
        {
            
        }

        private void OnExpWillChange(object sender, object args)
        {
            var vce = args as ValueChangeException;
            vce.AddModifier(new ClampValueModifier(EXP, MAX_EXPERIENCE, int.MaxValue));
        }
        
        private void OnExpDidChange(object sender, object args)
        {
            // update the LVL stat when the character receives new EXP
            _details.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
        }

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
    }
}