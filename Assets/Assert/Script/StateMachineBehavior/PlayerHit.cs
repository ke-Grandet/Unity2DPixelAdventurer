using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // 调用玩家战败方法
        animator.gameObject.GetComponent<PlayerController>().Defeated();

    }

}
