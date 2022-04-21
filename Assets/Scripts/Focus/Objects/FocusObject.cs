using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Focus", menuName = "Focus")]
public class FocusObject : ScriptableObject
{
    [SerializeField] bool damagePlayer;
    [SerializeField] bool damageMinion;
    [SerializeField] ElementObject[] elementsToConsume;

    public void PerformFocus()
    {
        if(damagePlayer && damageMinion)
        {
            PerformPlayer();
            PerformMinion();
        }
        else if(damagePlayer)
        {
            PerformPlayer();
        }
        else if(damageMinion)
        {
            PerformMinion();
        }
    }

    private void PerformPlayer()
    {
        GameManager.Instance.Player.Stats.TakeDamage((int) GameManager.Instance.Enemy.Stats.Stats["FocusDamage"].value, GameManager.Instance.Enemy.AffinityType, GameManager.Instance.Player);
    }

    private void PerformMinion()
    {

    }
}
