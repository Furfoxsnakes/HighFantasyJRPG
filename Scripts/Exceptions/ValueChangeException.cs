using System.Collections.Generic;
using HighFantasyJRPG.Scripts.Exceptions.Modifiers;

namespace HighFantasyJRPG.Scripts.Exceptions

public class ValueChangeException : BaseException
{
    public readonly float FromValue;
    public readonly float ToValue;
    public float Delta => ToValue - FromValue;
    private List<ValueModifier> _modifiers;
    
    public ValueChangeException(float fromValue, float toValue) : base(true)
    {
        FromValue = fromValue;
        ToValue = toValue;
    }

    public void AddModifier(ValueModifier modifier)
    {
        if (_modifiers == null)
            _modifiers = new List<ValueModifier>();
        _modifiers.Add(modifier);
    }

    public float GetModifiedValue()
    {
        var value = ToValue;

        if (_modifiers == null) return value;

        _modifiers.Sort(Compare);

        for (var i = 0; i < _modifiers.Count; ++i)
            value = _modifiers[i].Modify(value);

        return value;
    }

    private int Compare(ValueModifier x, ValueModifier y)
    {
        return x.SortOrder.CompareTo(y.SortOrder);
    }
}