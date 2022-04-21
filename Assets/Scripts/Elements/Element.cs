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

    public Element()
    {
        name = "";
        id = -1;
        affinityType = AffinityTypes.None;
    }

    public Element(ElementObject element)
    {
        name = element.name;
        id = element.ID;

        if(element.AffinityTypes.Length < 1)
        {
            affinityType = AffinityTypes.None;
        }
        else
        {
            affinityType = element.AffinityTypes[0]; // Might have to change this later if matters.
        }
    }

    public void UpdateSlot(Element element)
    {
        name = element.name;
        id = element.ID;
        affinityType = element.affinityType;
    }
}
