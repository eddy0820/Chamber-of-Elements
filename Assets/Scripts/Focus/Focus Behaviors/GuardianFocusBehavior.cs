using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardianFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        if(GameManager.Instance.Enemy.AffinityType == focus.BehaviorEntries.Affinity1)
        {
            GameManager.Instance.Enemy.SwitchAffinity(focus.BehaviorEntries.Affinity2);
            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().runtimeAnimatorController = focus.BehaviorEntries.AnimController2;
        }
        else
        {
            GameManager.Instance.Enemy.SwitchAffinity(focus.BehaviorEntries.Affinity1);
            GameManager.Instance.Enemy.GetComponentInChildren<Animator>().runtimeAnimatorController = focus.BehaviorEntries.AnimController1;
        }

        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }
}
