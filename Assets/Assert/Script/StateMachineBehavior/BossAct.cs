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

    //private bool isAttackHappend = false;  // �����Ƿ���

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // �ڶ�����ʼʱ����
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.IsName(MOVE_LEFT))
        //{
        //    Debug.Log("BOSS���������ƶ�");
        //}
        //if (stateInfo.IsName(MOVE_RIGHT))
        //{
        //    Debug.Log("BOSS���������ƶ�");
        //}
        //if (stateInfo.IsName(ATTACK_LEFT))
        //{
        //    Debug.Log("BOSS�������󹥻�");
        //}
        //if (stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    Debug.Log("BOSS�������ҹ���");
        //}
        //if (stateInfo.IsName(DEFEATED))
        //{
        //    Debug.Log("BOSS������");
        //}
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // �ڶ�����ʼ�Ͷ�������֮���ÿһ֡������
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ڹ�����������ʱ
        //if (stateInfo.IsName(ATTACK_LEFT) || stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    // ��׼��ʱ��normalizeTime��С������Ϊ0.5ʱ��ʾ���Ž�����50%������������һ��
        //    if (stateInfo.normalizedTime % 1 > 0.5 && !isAttackHappend)
        //    {
        //        // ���ù�������
        //        animator.gameObject.GetComponent<BossController>().Attack();
        //        isAttackHappend = true;

        //    }
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // �ڶ�������ʱ������
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ڹ�����������ʱ
        //if (stateInfo.IsName(ATTACK_LEFT) || stateInfo.IsName(ATTACK_RIGHT))
        //{
        //    // ��λ������־
        //    isAttackHappend = false;
        //    // ������������Ϊ����֮��
        //    if (Random.Range(0, 3) > 1)
        //    {
        //        // �����ƶ�
        //        animator.gameObject.GetComponent<BossController>().RandomAct(0);
        //    }
        //    else
        //    {
        //        // ���й���
        //        animator.gameObject.GetComponent<BossController>().RandomAct(2);
        //    }
        //}
        // ��ս�ܶ���������
        //if (stateInfo.IsName(DEFEATED))
        //{
        //    // ����BOSSս�ܷ���
        //    animator.gameObject.GetComponent<BossController>().Defeated();
        //}
    }


    // ���������ò���

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
