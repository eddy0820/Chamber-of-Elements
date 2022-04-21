using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementTextDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        if(GameManager.Instance.mouseElement.obj == null)
        {
            text.text = "";
        }
        else
        {
            text.text = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Description;
        }

    }
}
