using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Element", menuName = "Elements/Utility")]
public class UtilityElementObject : ElementObject
{
    [Header("Healing")]
    [SerializeField] float healAmount = -1;
    public float HealAmount => healAmount;
    [SerializeField] bool doHealInBehavior;
    public bool DoHealInBehavior => doHealInBehavior;

    private void Awake()
    {
        SetType(ElementTypes.Utility);
    }
}
