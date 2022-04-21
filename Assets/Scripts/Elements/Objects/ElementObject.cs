using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementObject : ScriptableObject
{
    public new string name = "New Element Name";
    public int ID = -1;
    [SerializeField] ElementTypes type;
    public ElementTypes Type => type;
    [SerializeField] AffinityTypes[] affinityTypes;
    public AffinityTypes[] AffinityTypes => affinityTypes;
    [SerializeField] Sprite elementTexture;
    public Sprite ElementTexture => elementTexture;

    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;

    [SerializeField] float damage = -1;
    public float Damage => damage;

    public void SetType(ElementTypes _type)
    {
        type = _type;
    }
}
