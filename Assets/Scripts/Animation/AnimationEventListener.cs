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
        }
        else
        {
            if(GameManager.Instance.Enemy.Stats.Stats["CanFocus"].value > 0)
            {
                if(UnityEngine.Random.Range(0, 101) >= GameManager.Instance.Enemy.Stats.Stats["FocusHitChance"].value)
                {
                    Debug.Log("Focus Miss");
                }
                else
                {
                    ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.PerformFocus();
                    ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
                }

                enemyTurnGameState.currentFocusCounter = 0;
            }
            else
            {
                DoEnemyAttackHelper();
            }
            
        }
    }

    private void DoEnemyAttackHelper()
    {
        if(UnityEngine.Random.Range(0, 101) > Player.Instance.Stats.Stats["HitChance"].value)
        {
            Debug.Log("Miss");
        }
        else if(GameManager.Instance.IsImmuneAffinity(Player.Instance, GameManager.Instance.Enemy.AffinityType) == false)
        {
            Player.Instance.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, Player.Instance);
            ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);
        }
    }

    public void EnemyFinishedAttacking()
    {
        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }
}
