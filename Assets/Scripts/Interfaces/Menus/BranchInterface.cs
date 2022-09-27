using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BranchInterface : AbstractGameInterface
{
    Battle battle;

    public void Init(Battle _battle)
    {
        battle = _battle;

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterBranch(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitBranch(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerClick, (data) => { OnClickBranch(gameObject, (PointerEventData)data); });
    }

    private void OnEnterBranch(GameObject gameObject)
    {

    }

    private void OnExitBranch(GameObject gameObject)
    {

    }

    private void OnClickBranch(GameObject gameObject, PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            PathSelectionScreenController.Instance.SelectBattle(battle);  //////////////////////////////////
            // you were about to do actual selecting and going into battle
            // figure out how to save this scene inbetween battles
        }
    }

    protected override void Initialize() {}

    protected override void OnAwake() {}

    protected override void UpdateInterface() {}
}
