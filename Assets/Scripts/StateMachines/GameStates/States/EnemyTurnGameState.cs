using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnGameState : GameState
{
    [SerializeField] GameObject focusUI;

    [Space(15)]
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

        foreach(KeyValuePair<int, System.Action> action in GameManager.Instance.Enemy.actionsToDoStartOfEveryTurn)
        {
            action.Value.Invoke();
        }
    }

    private void Attack()
    {
        if(!hasAttacked)
        {
            if(focusUI.gameObject.activeInHierarchy)
            {
                focusUI.gameObject.SetActive(false);
            }

            if(currentFocusCounter < GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value)
            {
                GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
                hasAttacked = true;
                
                currentFocusCounter++;
            }
            else
            {
                if(GameManager.Instance.Enemy.Stats.Stats["CanFocus"].value > 0)
                {
                    if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["FocusHitChance"].value)
                    {
                        Debug.Log("Focus Miss");
                    }
                    else
                    {
                        GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Focus");
                    }

                    currentFocusCounter = 0;
                    hasAttacked = true;
                }
                else
                {
                    GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
                    hasAttacked = true;
                }
                
            }

            
        }
        
        /* ACTUAL ATTACK TAKES PLACE IN ANIMATION EVENT LISTENER */
 
        /* FINISHED ATTACKING BOOL IS SWITCHED TO TRUE IN ANIMATION EVENT LISTENER */
    }

    public override void OnExitState()
    {
        foreach(KeyValuePair<int, System.Action> action in GameManager.Instance.Enemy.actionsToDoEndOfEveryTurn)
        {
            action.Value.Invoke();

            if(action.Key == 1000)
            {
                break;
            }
        }

        if(currentFocusCounter == GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value && GameManager.Instance.Enemy.Stats.Stats["CanFocus"].value > 0)
        {
            focusUI.SetActive(true);
        } 
    }
}
