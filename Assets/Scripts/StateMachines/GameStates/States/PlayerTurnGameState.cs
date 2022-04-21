using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnGameState : GameState
{
    bool goToNextState;

    public override State RunCurrentState()
    {
        if(goToNextState)
        {
            goToNextState = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            return gameStateManager.enemyTurnGameState;
        }
        else
        {
            return this;
        }
    }

    public void Attack(AffinityTypes damageType, int value)
    {
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy);

        GameObject.Find("Slash Animation").GetComponent<Animator>().SetTrigger("Attack");

        goToNextState = true;
    }
}
