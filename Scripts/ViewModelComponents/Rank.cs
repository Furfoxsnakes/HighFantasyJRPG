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

        public int LVL => _stats[StatTypes.LVL];

        public float LevelPercent => (float) (LVL - MIN_LEVEL) / (MAX_LEVEL - MIN_LEVEL);

        public int EXP
        {
            get => _stats[StatTypes.EXP];
            set => _stats[StatTypes.EXP] = value;
        }
        
        private Stats _stats;

        public override void _Ready()
        {
            _stats = Owner.GetNode<Stats>("Stats");
        }

        public override void _EnterTree()
        {
            this.AddObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), _stats);
            this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), _stats);
        }

        private void OnExpWillChange(object sender, object args)
        {
            var vce = args as ValueChangeException;
            vce.AddModifier(new ClampValueModifier(EXP, MAX_EXPERIENCE, int.MaxValue));
        }
        
        private void OnExpDidChange(object sender, object args)
        {
            
        }
    }
}