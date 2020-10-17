namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public class MultiplyValueModifier : ValueModifier
    {
        public readonly float MultiplyBy;
        
        public MultiplyValueModifier(float multiplyBy, int sortOrder) : base(sortOrder)
        {
            MultiplyBy = multiplyBy;
        }

        public override float Modify(float value)
        {
            return value * MultiplyBy;
        }
    }
}