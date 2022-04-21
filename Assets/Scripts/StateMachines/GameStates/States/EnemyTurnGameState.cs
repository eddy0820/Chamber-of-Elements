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
            GameManager.Instance.ElementSlotsInv.ReRollElements();
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            return gameStateManager.playerTurnGameState;
        }
        else
        {
            Attack();
            return this;
        }
    }

    private void Attack()
    {
        if(!hasAttacked)
        {
            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
            hasAttacked = true;
        }
        
        /* ACTUAL ATTACK TAKES PLACE IN ANIMATION EVENT LISTENER */
 
        /* FINISHED ATTACKING BOOL IS SWITCHED TO TRUE IN ANIMATION EVENT LISTENER */
    }
}
