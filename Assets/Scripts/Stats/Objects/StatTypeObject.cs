using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
public class StatTypeObject : ScriptableObject
{
    [SerializeField] new string name = "New Stat Type Name";
    [SerializeField] float defaultValue = 0f;
    public string Name => name;
    public float DefaultValue => defaultValue;
}
