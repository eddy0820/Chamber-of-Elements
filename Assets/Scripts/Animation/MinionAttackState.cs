using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.gameObject.GetComponent<MinionTurnGameState>().finishedAttacking = true;
    }
}
