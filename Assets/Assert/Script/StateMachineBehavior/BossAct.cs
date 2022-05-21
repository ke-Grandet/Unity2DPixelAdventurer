using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAct : StateMachineBehaviour
{
    //private const string MOVE_LEFT = "boss_move_left";
    //private const string MOVE_RIGHT = "boss_move_right";
    //private const string ATTACK_LEFT = "boss_attack_left";
    //private const string ATTACK_RIGHT = "boss_attack_right";
    //private const string DEFEATED = "boss_defeated";

    //private bool isAttackHappend = false;  // 攻击是否发生

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // 在动画开始时调用
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.IsName(MOVE_LEFT))
        //{
        //    Debug.Log("BOSS正在向左移动");
        //}
        //if (stateInfo.IsName(MOVE_RIGHT))
        //{
        //    Debug.Log("BOSS正在向右移动");
        //}
        //if (stateInfo.IsName(ATTACK_LEFT))
        //{
        //    Debug.Log("BOSS正在向左攻击");
        //}
        //if (stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    Debug.Log("BOSS正在向右攻击");
        //}
        //if (stateInfo.IsName(DEFEATED))
        //{
        //    Debug.Log("BOSS被击败");
        //}
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // 在动画开始和动画结束之间的每一帧被调用
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 在攻击动画进行时
        //if (stateInfo.IsName(ATTACK_LEFT) || stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    // 标准化时间normalizeTime的小数部分为0.5时表示播放进度是50%，即动画进行一半
        //    if (stateInfo.normalizedTime % 1 > 0.5 && !isAttackHappend)
        //    {
        //        // 调用攻击方法
        //        animator.gameObject.GetComponent<BossController>().Attack();
        //        isAttackHappend = true;

        //    }
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // 在动画结束时被调用
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 在攻击动画结束时
        //if (stateInfo.IsName(ATTACK_LEFT) || stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    // 复位攻击标志
        //    isAttackHappend = false;
        //    // 继续攻击概率为三分之二
        //    if (Random.Range(0, 3) > 1)
        //    {
        //        // 进行移动
        //        animator.gameObject.GetComponent<BossController>().RandomAct(0);
        //    }
        //    else
        //    {
        //        // 进行攻击
        //        animator.gameObject.GetComponent<BossController>().RandomAct(2);
        //    }
        //}
        // 在战败动画结束后
        //if (stateInfo.IsName(DEFEATED))
        //{
        //    // 调用BOSS战败方法
        //    animator.gameObject.GetComponent<BossController>().Defeated();
        //}
    }


    // 下面两个用不到

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
