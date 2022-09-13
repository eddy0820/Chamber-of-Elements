using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnGameState : GameState
{
    [SerializeField] float attackStartDelay = 1.5f;
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
            hasAttacked = true;

            if(focusUI.gameObject.activeInHierarchy)
            {
                focusUI.gameObject.SetActive(false);
            }

            StartCoroutine(DoAttack());
        }
        
        /* ACTUAL ATTACK TAKES PLACE IN ANIMATION EVENT LISTENER */
 
        /* FINISHED ATTACKING BOOL IS SWITCHED TO TRUE IN ANIMATION EVENT LISTENER */
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(attackStartDelay);

        if(currentFocusCounter < GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value)
        {
            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
                
            currentFocusCounter++;
        }
        else
        {
            if(GameManager.Instance.Enemy.Stats.Stats["CanFocus"].value > 0)
            {
                if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["FocusHitChance"].value)
                {
                    foreach(CharacterEntry characterEntry in ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.BehaviorEntries.FocusAffectedCharacters)
                    {
                        Character character = GameManager.Instance.ConvertCharacterEntry(characterEntry);

                        if(character.CharacterObject != null)
                        {
                            DamageIndicatorController.Instance.DoMissIndicator(character.transform.position);
                        }
                    }    
                }
                else
                {
                    GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Focus");
                }

                currentFocusCounter = 0;
            }
            else
            {
                GameManager.Instance.Enemy.GetComponentInChildren<Animator>().SetTrigger("Attack");
            }
        } 

        yield break;
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
