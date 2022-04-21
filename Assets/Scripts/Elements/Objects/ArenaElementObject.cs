using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Arena Element", menuName = "Elements/Arena")]
public class ArenaElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Arena);
    }
}
