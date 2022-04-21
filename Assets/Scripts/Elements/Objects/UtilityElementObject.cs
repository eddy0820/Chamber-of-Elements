using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Element", menuName = "Elements/Utility")]
public class UtilityElementObject : ElementObject
{
    private void Awake()
    {
        SetType(ElementTypes.Utility);
    }
}
