using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionTurnGameState : GameState
{
    [SerializeField] GameObject focusUI;

    [Space(15)]
    [ReadOnly] public int currentFocusCounter = 0;
    
    [System.NonSerialized] public bool hasUsedLife;
    [System.NonSerialized] public bool hasUsedRadiance;

    [System.NonSerialized] public bool finishedAttacking;
    bool hasAttacked;


    public override State RunCurrentState()
    {
        if(finishedAttacking)
        {
            finishedAttacking = false;
            hasAttacked = false;

            return gameStateManager.enemyTurnGameState;
        }
        else
        {
            Attack();
            return this;
        }
    }

    public override void OnEnterState()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        foreach(KeyValuePair<int, System.Action> action in Player.Instance.Minion.actionsToDoStartOfEveryTurn)
        {
            action.Value.Invoke();
        }
    }

    private void Attack()
    {
        if(!hasAttacked)
        {
            if(Player.Instance.Minion.Stats.Stats.ContainsKey("FocusCooldown") && Player.Instance.Minion.Stats.Stats.ContainsKey("CanFocus") && Player.Instance.Minion.Stats.Stats.ContainsKey("FocusHitChance"))
            {
                if(focusUI.activeInHierarchy)
                {
                    focusUI.SetActive(false);
                }
            }

            Player.Instance.Minion.GetComponentInChildren<Animator>().SetTrigger("Attack");
            hasAttacked = true;
        }
    }

    public override void OnExitState()
    {
        foreach(KeyValuePair<int, System.Action> action in Player.Instance.Minion.actionsToDoEndOfEveryTurn)
        {
            action.Value.Invoke();

            if(action.Key == 1000)
            {
                break;
            }
        }

        if(Player.Instance.Minion.Stats.Stats.ContainsKey("FocusCooldown") && Player.Instance.Minion.Stats.Stats.ContainsKey("CanFocus") && Player.Instance.Minion.Stats.Stats.ContainsKey("FocusHitChance"))
        {
            if(currentFocusCounter == Player.Instance.Minion.Stats.Stats["FocusCooldown"].value && Player.Instance.Minion.Stats.Stats["CanFocus"].value > 0)
            {
                focusUI.gameObject.SetActive(true);
            } 
        }
    } 
}
