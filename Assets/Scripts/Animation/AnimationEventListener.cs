using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public void EnemyAttack()
    {
        if(GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().currentFocusCounter < GameManager.Instance.Enemy.Stats.Stats["FocusCooldown"].value)
        {
            GameManager.Instance.Player.Stats.TakeDamage((int) GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, GameManager.Instance.Player.CharacterObject.Name);
            GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().currentFocusCounter++;
        }
        else
        {
            ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.PerformFocus();
            GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().currentFocusCounter = 0;
        }
    }

    public void EnemyFinishedAttacking()
    {
        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }
}
