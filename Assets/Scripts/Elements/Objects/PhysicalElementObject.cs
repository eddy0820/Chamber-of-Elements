using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Physical Element", menuName = "Elements/Physical")]
public class PhysicalElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Physical);
    }
}
