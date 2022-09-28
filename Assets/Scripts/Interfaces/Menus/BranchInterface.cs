using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BranchInterface : AbstractGameInterface
{
    [SerializeField] GameObject battleTooltip;
    Battle battle;
    bool hidden = false;

    public void Init(Battle _battle)
    {
        battle = _battle;

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterBranch(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitBranch(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerClick, (data) => { OnClickBranch(gameObject, (PointerEventData)data); });

        battleTooltip.transform.GetChild(2).GetComponent<Image>().sprite = battle.Enemy.Sprite;
        battleTooltip.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = battle.Enemy.Name;
    }

    private void OnEnterBranch(GameObject obj)
    {
        if(hidden == false)
        {
            battleTooltip.SetActive(true);
        }
    }

    private void OnExitBranch(GameObject obj)
    {
        if(hidden == false)
        {
            battleTooltip.SetActive(false);
        }
    }

    private void OnClickBranch(GameObject obj, PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            if(hidden == false)
            {
                PathSelectionScreenController.Instance.SelectBattle(battle);
                obj.transform.GetChild(0).gameObject.SetActive(true);
                transform.parent.GetComponent<BranchGridController>().RemoveSelectedImageFromBranchSelection(obj);
            }
        }
    }

    public void HideBattle()
    {
        hidden = true;
        GetComponent<Image>().color = PathSelectionScreenController.Instance.BattleHiddenColor;
    }

    protected override void Initialize() {}

    protected override void OnAwake() {}

    protected override void UpdateInterface() {}
}
