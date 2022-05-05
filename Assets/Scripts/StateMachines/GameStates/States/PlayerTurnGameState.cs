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
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            hasUsedLife = false;
            hasUsedRadiance = false;
            return gameStateManager.enemyTurnGameState;
        }
        else
        {
            return this;
        }
    }

    public void Attack(AffinityTypes damageType, float value)
    {
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy);
        ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);

        GameObject.Find("Slash Animation").GetComponent<Animator>().SetTrigger("Attack");

        goToNextState = true;
    }
}
