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
    }

    protected abstract void OnAwake();
    protected abstract void Initialize();
    protected abstract void UpdateInterface();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
