using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveObject : ScriptableObject
{
    [SerializeField] protected new string name = "New Passive Name";

    public abstract void TakeAffect(CharacterStats stats);

    public abstract void RemoveAffect(CharacterStats stats);
}
