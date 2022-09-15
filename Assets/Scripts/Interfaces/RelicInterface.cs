using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RelicInterface : AbstractGameInterface
{
    public static RelicInterface Instance {get; private set; }
    [SerializeField] GameObject relicDescription;

    protected override void OnAwake()
    {
        Instance = this;
        
        relicDescription.GetComponent<TextMeshProUGUI>().text = "";
    }

    protected override void Initialize()
    {
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterRelicSlot(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitRelicSlot(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerClick, (data) => { OnClickRelicSlot(gameObject, (PointerEventData)data); });
    }

    private void OnEnterRelicSlot(GameObject obj)
    {
        if(Player.Instance.HasRelic)
        {
            relicDescription.GetComponent<TextMeshProUGUI>().text = Player.Instance.Relic.RelicObject.Description;
        } 
    }

    private void OnExitRelicSlot(GameObject obj)
    {
        relicDescription.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void OnClickRelicSlot(GameObject obj, PointerEventData eventData)
    {
        if(GameStateManager.Instance.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(Player.Instance.Relic.RelicObject.Behavior is IOnClickRelic)
                {
                    ((IOnClickRelic) Player.Instance.Relic.RelicObject.Behavior).OnRelicClick(Player.Instance.Relic.RelicObject, GameManager.Instance.mouseElement.element);
                }
            }
            else if(eventData.button == PointerEventData.InputButton.Right) {}
        }

    }

    protected override void UpdateInterface() {}
}
