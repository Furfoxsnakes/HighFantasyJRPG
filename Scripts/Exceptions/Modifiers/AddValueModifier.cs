namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public class AddValueModifier : ValueModifier
    {
        public readonly float ToAdd;
        
        public AddValueModifier(float toAdd, int sortOrder) : base(sortOrder)
        {
            ToAdd = toAdd;
        }

        public override float Modify(float value)
        {
            return value + ToAdd;
        }
    }
}