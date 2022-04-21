using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Characters/Enemy")]
public class EnemyObject : CharacterObject 
{
    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;
}
