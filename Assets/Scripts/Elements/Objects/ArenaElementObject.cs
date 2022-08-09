using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Arena Element", menuName = "Elements/Arena")]
public class ArenaElementObject : ElementObject
{
    public override void OnAwake()
    {
        SetType(ElementTypes.Arena);
    }
}
