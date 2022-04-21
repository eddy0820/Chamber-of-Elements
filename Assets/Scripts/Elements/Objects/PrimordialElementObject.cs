using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Primordial Element", menuName = "Elements/Primordial")]
public class PrimordialElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Primordial);
    }
}
