using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elemental Element", menuName = "Elements/Elemental")]
public class ElementalElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Elemental);
    }
}
