using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnGameState : GameState
{
    [ReadOnly] public int currentFocusCounter = 0;
    [ReadOnly] public int cannotFocusCounter = -1;
    [System.NonSerialized] public bool finishedAttacking;
    bool hasAttacked;

    

    public override State RunCurrentState()
    {
        if(finishedAttacking)
        {
            finishedAttacking = false;
            hasAttacked = false;

            if(gameStateManager.ReRollSpecific)
            {
                GameManager.Instance.ElementSlotsInv.ReRollSpecificElement(gameStateManager.ReRollElement);
            }
            else
            {
                GameManager.Instance.ElementSlotsInv.ReRollElements();
            }

            gameStateManager.UnsetReRollSpecificNextTurn();
            
            UnityEngine.Cursor.lockState = CursorLockMode.None;

            if(currentFocusCounter == GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value)
            {
                GameManager.Instance.InfoCanvas.transform.GetChild(4).gameObject.SetActive(true);
            }

            GameManager.Instance.WeatherStateManager.turnsTillClearWeather++;

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
            if(GameManager.Instance.InfoCanvas.transform.GetChild(4).gameObject.activeInHierarchy)
            {
                GameManager.Instance.InfoCanvas.transform.GetChild(4).gameObject.SetActive(false);
            }

            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
            hasAttacked = true;
        }
        
        /* ACTUAL ATTACK TAKES PLACE IN ANIMATION EVENT LISTENER */
 
        /* FINISHED ATTACKING BOOL IS SWITCHED TO TRUE IN ANIMATION EVENT LISTENER */
    }
}
