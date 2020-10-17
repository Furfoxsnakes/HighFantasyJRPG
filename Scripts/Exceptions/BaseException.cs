namespace HighFantasyJRPG.Scripts.Exceptions
{
    public class BaseException
    {
        public bool Toggle { get; private set; }
        private bool _defaultToggle;

        public BaseException(bool defaultToggle)
        {
            Toggle =_defaultToggle = defaultToggle;
        }

        public void FlipToggle()
        {
            Toggle = !_defaultToggle;
        }
    }
}