using Godot;

namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public class MaxValueModifier : ValueModifier
    {
        public readonly float Max;
        
        public MaxValueModifier(float max, int sortOrder) : base(sortOrder)
        {
            Max = max;
        }

        public override float Modify(float value)
        {
            return Mathf.Max(value, Max);
        }
    }
}