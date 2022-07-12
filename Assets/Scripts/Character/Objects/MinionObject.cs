using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Characters/Minion")]
public class MinionObject : CharacterObject 
{
    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;
    
    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;
}
