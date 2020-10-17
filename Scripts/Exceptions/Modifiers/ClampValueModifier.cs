using Godot;

namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public class ClampValueModifier : ValueModifier
    {
        public readonly float Min;
        public readonly float Max;
        
        public ClampValueModifier(float min, float max, int sortOrder) : base(sortOrder)
        {
            Min = min;
            Max = max;
        }

        public override float Modify(float value)
        {
            return Mathf.Clamp(value, Min, Max); 
        }
    }
}