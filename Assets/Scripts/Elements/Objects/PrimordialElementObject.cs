using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Primordial Element", menuName = "Elements/Primordial")]
public class PrimordialElementObject : ElementObject
{
    public override void OnAwake()
    {
        SetType(ElementTypes.Primordial);
    }
}
