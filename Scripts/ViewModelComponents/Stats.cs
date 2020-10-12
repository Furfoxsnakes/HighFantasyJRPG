using Godot;

public class Stats : Node
{
    [Signal]
    public delegate void StatsChanged(StatTypes type, int oldValue, int newValue);

    public int this[StatTypes type]
    {
        get => _data[(int) type];
        set => SetValue(type, value);
    }
    private int[] _data = new int[(int) StatTypes.Count];

    private void SetValue(StatTypes type, int value)
    {
        var oldValue = _data[(int) type];
        if (value == oldValue) return;

        _data[(int) type] = value;
        EmitSignal(nameof(StatsChanged), type, oldValue, value);
    }
}