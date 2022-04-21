public class StatModifier
{
    // The readonly keyword means we can't alter that variable except when we
    // are in the constructor of the class or in the declaration itself. 
    public readonly float value;
    public readonly StatModifierTypes type;
    public readonly int order;
    public readonly object source;

    // Constructor that requires the value, type, order, and source.
    public StatModifier(float _value, StatModifierTypes _type, int _order, object _source)
    {
        value = _value;
        type = _type;
        order = _order;
        source = _source;
    }

    // These constructors all call the one above.
    // Both order and source are optional.

    // Constructor that requires only the value and type, 
    // defaulting order to the type index and source to null.
    public StatModifier(float _value, StatModifierTypes _type) : this (_value, _type, (int) _type, null) {}
    // Constructor that requires the value, type, and order,
    // defaulting source to null.
    public StatModifier(float _value, StatModifierTypes _type, int _order) : this (_value, _type, _order, null) {}
    // Constructor that requires the value, type, and source,
    // defaulting order to the type index.
    public StatModifier(float _value, StatModifierTypes _type, object _source) : this (_value, _type, (int) _type, _source) {}
}
