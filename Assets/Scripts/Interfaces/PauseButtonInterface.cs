using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButtonInterface : AbstractGameInterface
{
    [SerializeField] Vector3 startingScale = Vector3.one;
    [SerializeField] Vector3 hoverScale = Vector3.one;
    protected override void OnAwake() {}

    protected override void Initialize()
    {
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterPauseButton(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitPauseButton(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerClick, (data) => { OnClickPauseButton(gameObject, (PointerEventData)data); });
    }

    private void OnEnterPauseButton(GameObject obj)
    {
        gameObject.GetComponent<RectTransform>().localScale = hoverScale;
    }

    private void OnExitPauseButton(GameObject obj)
    {
        gameObject.GetComponent<RectTransform>().localScale = startingScale;
    }

    private void OnClickPauseButton(GameObject obj, PointerEventData eventData)
    {
        PauseMenu.Instance.PauseGame();
    }

    protected override void UpdateInterface() {}

    protected override void UpdateMouseObjectTransform() {}
}
