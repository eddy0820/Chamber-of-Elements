using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BranchGridController : MonoBehaviour
{
    BattleSelection battleSelection;

    public void Init(BattleSelection _battleSelection)
    {
        battleSelection = _battleSelection;
        
        string text = "";

        switch(battleSelection.BranchBattleType)
        {
            case BattleTypes.Starting:
                text = "Starting\nBattle";
                break;
            case BattleTypes.Battle:
                text = "Battle";
                break;
            case BattleTypes.EliteBattle:
                text = "Elite\nBattle";
                break;
            case BattleTypes.MiniBoss:
                text = "Miniboss";
                break;
            case BattleTypes.BattlePlus:
                text = "Battle+";
                break;
            case BattleTypes.EliteBattlePlus:
                text = "Elite\nBattle+";
                break;
            case BattleTypes.Boss:
                text = "Boss";
                break;
        }

        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void HideBattles()
    {
        BranchInterface[] branches = GetComponentsInChildren<BranchInterface>();
        
        foreach(BranchInterface branch in branches)
        {
            branch.HideBattle();
        }
    }

    public void RemoveSelectedImageFromBranchSelection(GameObject exception)
    {
        BranchInterface[] branches = GetComponentsInChildren<BranchInterface>();

        foreach(BranchInterface branch in branches)
        {
            if(branch.gameObject != exception)
            {
                branch.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
