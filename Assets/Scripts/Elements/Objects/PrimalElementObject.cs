using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Primal Element", menuName = "Elements/Primal")]
public class PrimalElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Primal);
    }
}
