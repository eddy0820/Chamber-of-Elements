using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class AbstractGameInterface : MonoBehaviour
{
    private void Awake()
    {
        OnAwake(); 
    }

    private void Start()
    {
        Initialize();
    }

    private void LateUpdate()
    {
        UpdateInterface();
        UpdateMouseObjectTransform();
    }

    protected abstract void OnAwake();
    protected abstract void Initialize();
    protected abstract void UpdateInterface();
    protected abstract void UpdateMouseObjectTransform();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}

[System.Serializable]
public class MouseElement
{
    [ReadOnly] public GameObject obj;
    [ReadOnly] public Element element;
    [ReadOnly] public GameObject hoverObj;
    [ReadOnly] public Element hoverElement;
    [ReadOnly] public GameObject cursorTextObj;

    public MouseElement()
    {
        obj = null;
        element = new Element();
        hoverObj = null;
        hoverElement = new Element();
        cursorTextObj = null;
    }

    public void RemoveMouseElement(Action<GameObject> Destroy)
    {
        Destroy(obj);
        element.UpdateSlot(new Element());
    }

    public void RemoveMouseCursorText(Action<GameObject> Destroy)
    {
        Destroy(cursorTextObj);
        cursorTextObj = null;
    }
}
