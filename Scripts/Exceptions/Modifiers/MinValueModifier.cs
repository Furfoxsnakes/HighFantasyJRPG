using Godot;

namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public class MinValueModifier : ValueModifier
    {
        public readonly float Min;
        
        public MinValueModifier(float min, int sortOrder) : base(sortOrder)
        {
            Min = min;
        }

        public override float Modify(float value)
        {
            return Mathf.Min(value, Min);
        }
    }
}