using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractElementBehavior : MonoBehaviour
{
    public abstract bool DoBehavior(ElementObject element, Character character);
}
