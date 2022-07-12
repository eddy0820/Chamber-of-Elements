using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnGameState : GameState
{
    bool goToNextState;
    [System.NonSerialized] public bool hasUsedLife;
    [System.NonSerialized] public bool hasUsedRadiance;

    public override State RunCurrentState()
    {
        if(goToNextState)
        {
            goToNextState = false;
            
            hasUsedLife = false;
            hasUsedRadiance = false;

            if(Player.Instance.MinionExists)
            {
                return gameStateManager.minionTurnGameState;
            }
            else
            {
                return gameStateManager.enemyTurnGameState;
            }
            
        }
        else
        {
            return this;
        }
    }

    public override void OnEnterState()
    {       
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        GameManager.Instance.turnCounter++;

        foreach(KeyValuePair<int, System.Action> action in Player.Instance.actionsToDoStartOfEveryTurn)
        {
            action.Value.Invoke();
        }

        DoReRoll();
    }

    public void Attack(AffinityTypes damageType, float value)
    {
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy, Player.Instance);

        ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);

        GameObject.Find("Slash Animation").GetComponent<Animator>().SetTrigger("Attack");

        goToNextState = true;
    }

    public override void OnExitState()
    {
        foreach(KeyValuePair<int, System.Action> action in Player.Instance.actionsToDoEndOfEveryTurn)
        {
            action.Value.Invoke();

            if(action.Key == 1000)
            {
                break;
            }
        }
    }

    private void DoReRoll()
    {
        if(GameStateManager.Instance.ReRollSpecific)
        {
            GameManager.Instance.ElementSlotsInv.ReRollSpecificElement(GameStateManager.Instance.ReRollElement);
        }
        else
        {
            GameManager.Instance.ElementSlotsInv.ReRollElements();
        }

        GameStateManager.Instance.UnsetReRollSpecificNextTurn();
    }

    public void GoToNextState()
    {
        goToNextState = true;
    }
    
}
