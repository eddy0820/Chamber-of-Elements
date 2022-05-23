using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Physical Element", menuName = "Elements/Physical")]
public class PhysicalElementObject : ElementObject
{
    public override void OnAwake()
    {
        SetType(ElementTypes.Physical);
    }
}
