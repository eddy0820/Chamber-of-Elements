using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnGameState : GameState
{
    [ReadOnly] public int currentFocusCounter = 0;
    [System.NonSerialized] public bool finishedAttacking;
    bool hasAttacked;

    public override State RunCurrentState()
    {
        if(finishedAttacking)
        {
            finishedAttacking = false;
            hasAttacked = false;

            return gameStateManager.playerTurnGameState;
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
    }

    private void Attack()
    {
        if(!hasAttacked)
        {
            if(GameManager.Instance.InfoCanvas.transform.GetChild(2).gameObject.activeInHierarchy)
            {
                GameManager.Instance.InfoCanvas.transform.GetChild(2).gameObject.SetActive(false);
            }

            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
            hasAttacked = true;
        }
        
        /* ACTUAL ATTACK TAKES PLACE IN ANIMATION EVENT LISTENER */
 
        /* FINISHED ATTACKING BOOL IS SWITCHED TO TRUE IN ANIMATION EVENT LISTENER */
    }

    public override void OnExitState()
    {
        foreach(KeyValuePair<int, System.Action> action in GameManager.Instance.Enemy.actionsToDoEveryTurn)
        {
            action.Value.Invoke();

            if(action.Key == 1000)
            {
                return;
            }
        }

        if(gameStateManager.enemyTurnGameState.currentFocusCounter == GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value && GameManager.Instance.Enemy.Stats.Stats["CanFocus"].value > 0)
        {
            GameManager.Instance.InfoCanvas.transform.GetChild(2).gameObject.SetActive(true);
        } 
    }
}
