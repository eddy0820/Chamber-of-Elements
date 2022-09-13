using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Characters/Minion")]
public class MinionObject : CharacterObject 
{
    [Header("Other")]

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;
    
    [Space(15)]

    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;

    [HorizontalLine(color: EColor.Gray, height: 2)]
    
    [SerializeField] List<PassiveEntryTarget> passivesToGiveCharacters = new List<PassiveEntryTarget>();
    public List<PassiveEntryTarget> PassivesToGiveCharacters => passivesToGiveCharacters;

    [System.Serializable]
    public class PassiveEntryTarget : PassiveEntry
    {
        public CharacterEntry character;
    }
}
