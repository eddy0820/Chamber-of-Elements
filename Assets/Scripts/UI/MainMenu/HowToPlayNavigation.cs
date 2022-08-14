using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayNavigation : MonoBehaviour
{
    [SerializeField] GameObject backArrow;

    public void BackButtonSelected()
    {
        backArrow.SetActive(true);
    }

    public void BackButtonDeselected()
    {
        backArrow.SetActive(false);
    }
}
