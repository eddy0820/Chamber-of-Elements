using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveObject : ScriptableObject
{
    [SerializeField] protected new string name = "New Passive Name";
    public string Name => name;
    [SerializeField] protected Sprite passiveTexture;
    public Sprite PassiveTexture => passiveTexture;

    [TextArea(15, 20)]
    [SerializeField] protected string description;
    public string Description => description;
    
    [Space(15)]
    [SerializeField] ChangeTypeEntry[] additiveTypes;
    public ChangeTypeEntry[] AdditiveTypes => additiveTypes;
    [SerializeField] ChangeTypeEntry[] removalTypes;
    public ChangeTypeEntry[] RemovalTypes => removalTypes;

    [Space(15)]
    [SerializeField] bool isPositiveEffect = true;
    public bool IsPositiveEffect => isPositiveEffect;

    protected StatModifier modifier;
    protected Action action;

    public abstract void TakeAffect(Character character);
    public abstract void RemoveAffect(Character character);

   
    [System.Serializable]
    public class ChangeTypeEntry
    {
        [SerializeField] AffinityTypes affinityType;
        public AffinityTypes AffinityType => affinityType;
        [SerializeField] PassiveObject passive;
        public PassiveObject Passive => passive;
        [SerializeField] float value = -1;
        public float Value => value;
    }
}
