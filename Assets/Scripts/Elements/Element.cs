using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Element
{
    [SerializeField] string name = "";
    public string Name => name;
    [SerializeField] int id = -1;
    public int ID => id;
    [SerializeField] AffinityTypes affinityType;
    public AffinityTypes AffinityType => affinityType;
    [SerializeField] AffinityTypes secondaryAffinityType;
    public AffinityTypes SecondaryAffinityType => secondaryAffinityType;

    public Element()
    {
        name = "";
        id = -1;
        affinityType = AffinityTypes.None;
        secondaryAffinityType = AffinityTypes.None;
    }

    public Element(ElementObject element)
    {
        name = element.name;
        id = element.ID;
        affinityType = element.AffinityType;
        secondaryAffinityType = element.SecondaryAffinityType;
    }

    public void UpdateSlot(Element element)
    {
        name = element.name;
        id = element.ID;
        affinityType = element.affinityType;
        secondaryAffinityType = element.SecondaryAffinityType;
    }
}
