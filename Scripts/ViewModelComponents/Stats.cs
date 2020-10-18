using System.Collections.Generic;
using Godot;
using HighFantasyJRPG.Scripts.Exceptions;

public class Stats : Node
{
    [Signal]
    public delegate void StatsChanged(StatTypes type, int oldValue, int newValue);

    private static readonly Dictionary<StatTypes, string>
        _willChangeNotifications = new Dictionary<StatTypes, string>();

    private static readonly Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();
    private readonly int[] _data = new int[(int) StatTypes.Count];

    public int this[StatTypes type]
    {
        get => _data[(int) type];
        set => SetValue(type, value, true);
    }

    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, $"Stats.{type}WillChange");
        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, $"Stats.{type}DidChange");
        return _didChangeNotifications[type];
    }

    public void SetValue(StatTypes type, int value, bool allowExceptions)
    {
        var oldValue = this[type];
        if (value == oldValue) return;

        if (allowExceptions)
        {
            var exc = new ValueChangeException(oldValue, value);
            this.PostNotification(WillChangeNotification(type), exc);

            value = Mathf.FloorToInt(exc.GetModifiedValue());

            // check if anything nullified the change
            if (exc.Toggle == false || value == oldValue) return;
        }

        _data[(int) type] = value;
        EmitSignal(nameof(StatsChanged), type, oldValue, value);
    }
}