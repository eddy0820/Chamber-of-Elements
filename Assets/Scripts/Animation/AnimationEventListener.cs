using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public void EnemyAttack()
    {
        EnemyTurnGameState enemyTurnGameState = GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>();

        if(enemyTurnGameState.currentFocusCounter < GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value)
        {
            DoEnemyAttackHelper();
            enemyTurnGameState.currentFocusCounter++;

            if(enemyTurnGameState.cannotFocusCounter > 0)
            {
                enemyTurnGameState.cannotFocusCounter--;
            }
        }
        else
        {
            if(!(enemyTurnGameState.cannotFocusCounter > 0))
            {
                ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.PerformFocus();
                ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
                enemyTurnGameState.currentFocusCounter = 0;
            }
            else
            {
                DoEnemyAttackHelper();
                enemyTurnGameState.cannotFocusCounter--;
            }
            
        }
    }

    private void DoEnemyAttackHelper()
    {
        GameManager.Instance.Player.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, GameManager.Instance.Player);
        ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);
    }

    public void EnemyFinishedAttacking()
    {
        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }
}
