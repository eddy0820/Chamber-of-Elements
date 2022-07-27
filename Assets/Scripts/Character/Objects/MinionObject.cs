using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Characters/Minion")]
public class MinionObject : CharacterObject 
{
    [Header("Minion")]
    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;
    
    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;
    [SerializeField] List<PassiveEntryTarget> passivesToGiveCharacters = new List<PassiveEntryTarget>();
    public List<PassiveEntryTarget> PassivesToGiveCharacters => passivesToGiveCharacters;

    [System.Serializable]
    public class PassiveEntryTarget : PassiveEntry
    {
        public CharacterEntry character;
    }
}
