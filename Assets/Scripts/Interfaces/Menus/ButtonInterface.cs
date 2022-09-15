using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInterface : AbstractGameInterface
{   
    [SerializeField] List<ButtonEntry> buttons;
    protected override void OnAwake() 
    {
        foreach(ButtonEntry buttonEntry in buttons)
        {
            AddEvent(buttonEntry.Button, EventTriggerType.PointerEnter, delegate { ButtonSelected(buttonEntry.Arrow); });
            AddEvent(buttonEntry.Button, EventTriggerType.PointerExit, delegate { ButtonDeselected(buttonEntry.Arrow); });
            AddEvent(buttonEntry.Button, EventTriggerType.PointerClick, (data) => { OnClick((PointerEventData)data, buttonEntry.Function, buttonEntry.Arrow); });
        }
    }

    protected override void Initialize() {}
    
    protected override void UpdateInterface() {}

    private void ButtonSelected(GameObject arrow)
    {
        arrow.SetActive(true);
    }

    private void ButtonDeselected(GameObject arrow)
    {
        arrow.SetActive(false);
    } 

    private void OnClick(PointerEventData data, ButtonEntry.ButtonEntryEvent function, GameObject arrow)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            function.Invoke(arrow);
        }
    }

    [System.Serializable]
    public class ButtonEntry
    {
        [SerializeField] GameObject button;
        public GameObject Button => button;
        [SerializeField] GameObject arrow;
        public GameObject Arrow => arrow;
        [SerializeField] ButtonEntryEvent function = new ButtonEntryEvent();
        public ButtonEntryEvent Function => function;
        
        [System.Serializable]
        public class ButtonEntryEvent : UnityEvent<GameObject> {}
    }
}
