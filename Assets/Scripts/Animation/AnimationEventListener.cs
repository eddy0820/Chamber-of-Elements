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
                if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["FocusHitChance"].value)
                {
                    Debug.Log("Focus Miss");
                }
                else
                {
                    ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.PerformFocus(GameManager.Instance.Enemy);
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
        if(!Player.Instance.MinionExists)
        {
            if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["HitChance"].value)
            {
                Debug.Log("Miss");
            }
            else if(Player.Instance.IsImmuneAffinity(GameManager.Instance.Enemy.AffinityType) == false)
            {
                Player.Instance.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, Player.Instance, GameManager.Instance.Enemy);
                ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);
            }
        }
        else
        {
            if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["HitChance"].value)
            {
                Debug.Log("Miss");
            }
            else if(Player.Instance.Minion.IsImmuneAffinity(GameManager.Instance.Enemy.AffinityType) == false)
            {
                Player.Instance.Minion.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, Player.Instance.Minion, GameManager.Instance.Enemy);
                ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);
            }
        } 
    }

    public void EnemyFinishedAttacking()
    {
        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }

    public void MinionAttack()
    {
        if(Player.Instance.Minion.Stats.Stats.ContainsKey("FocusCooldown") && Player.Instance.Minion.Stats.Stats.ContainsKey("CanFocus") && Player.Instance.Minion.Stats.Stats.ContainsKey("FocusHitChance"))
        {
            MinionTurnGameState minionTurnGameState = GameManager.Instance.gameObject.GetComponent<MinionTurnGameState>();

            if(minionTurnGameState.currentFocusCounter < Player.Instance.Minion.Stats.Stats["FocusCooldown"].value)
            {
                DoMinionAttackHelper();
                
                minionTurnGameState.currentFocusCounter++;
            }
            else
            {
                if(Player.Instance.Minion.Stats.Stats["CanFocus"].value > 0)
                {
                    if(UnityEngine.Random.Range(0, 101) > Player.Instance.Minion.Stats.Stats["FocusHitChance"].value)
                    {
                        Debug.Log("Focus Miss");
                    }
                    else
                    {
                        ((MinionObject) Player.Instance.Minion.CharacterObject).Focus.PerformFocus(Player.Instance.Minion);
                        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
                    }

                    minionTurnGameState.currentFocusCounter = 0;
                }
                else
                {
                    DoMinionAttackHelper();
                } 
            }
        }
        else
        {
            DoMinionAttackHelper();
        } 
    }

    private void DoMinionAttackHelper()
    {
        if(UnityEngine.Random.Range(0, 101) > Player.Instance.Minion.Stats.Stats["HitChance"].value)
        {
            Debug.Log("Miss");
        }
        else if(GameManager.Instance.Enemy.IsImmuneAffinity(Player.Instance.Minion.AffinityType) == false)
        {
            GameManager.Instance.Enemy.Stats.TakeDamage(Player.Instance.Minion.Stats.Stats["BasicAttack"].value, Player.Instance.Minion.AffinityType, GameManager.Instance.Enemy, Player.Instance.Minion);
            ScreenShakeBehavior.Instance.StartShake(0.7f, 0.3f, 7.5f);
        }
    }
}
