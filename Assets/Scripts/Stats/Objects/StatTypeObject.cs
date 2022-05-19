using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
public class StatTypeObject : ScriptableObject
{
    [SerializeField] new string name = "New Stat Type Name";
    [SerializeField] float defaultValue = 0f;

    [Space(15)]
    [SerializeField] float defaultMinValue = -1f;
    [SerializeField] float defaultMaxValue = -1f;
    
    public string Name => name;
    public float DefaultValue => defaultValue;
    public float DefaultMinValue => defaultMinValue;
    public float DefaultMaxValue => defaultMaxValue;

}
