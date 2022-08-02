using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTooltip : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    void LateUpdate()
    {
        gameObject.transform.position = Input.mousePosition + offset;
    }
}
