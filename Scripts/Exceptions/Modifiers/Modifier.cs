namespace HighFantasyJRPG.Scripts.Exceptions.Modifiers
{
    public abstract class Modifier
    {
        public readonly int SortOrder;

        public Modifier(int sortOrder)
        {
            SortOrder = sortOrder;
        }
    }
}