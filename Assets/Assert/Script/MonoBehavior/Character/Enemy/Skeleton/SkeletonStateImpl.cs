using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonSpace
{

    public class IdleState : IState
    {
        private readonly Skeleton skeleton;
        public IdleState(Skeleton skeleton) => this.skeleton = skeleton;
        private float timer = 0f;
        public void OnEnter()
        {
            Debug.Log("���þ�ֹ����");
            // ���ž�ֹ����
            skeleton._animator.Play("skeleton_idle");
        }

        public void OnUpdate()
        {
            // �ܵ��˺�ʱ�����ܻ�״̬
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // ׷����Χ�ڷ���Ŀ��ʱ���뷴Ӧ״̬
            if (skeleton.target != null
                && skeleton.target.position.x > skeleton.chasePoints[0].x
                && skeleton.target.position.x < skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.React);
                return;
            }
            // ��ʱ��������ֹʱ��ʱ�����ƶ�״̬
            timer += Time.deltaTime;
            if (timer > skeleton.idleTime)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Patrol);
                return;
            }
        }

        public void OnExit()
        {
            timer = 0f;
        }
    }

    public class PatrolState : IState
    {
        private readonly Skeleton skeleton;
        public PatrolState(Skeleton skeleton) => this.skeleton = skeleton;
        private int patrolIndex = 0;
        private Vector2 patrolTarget;
        public void OnEnter()
        {
            Debug.Log("��������Ѳ��");
            // ����Ѳ�߶���
            skeleton._animator.Play("skeleton_walk");
        }

        public void OnUpdate()
        {
            // �ܵ��˺�ʱ�����ܻ�״̬
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // ���ݵ�ǰѲ�ߵ�ȷ���ƶ�����
            patrolTarget = new(skeleton.patrolPoints[patrolIndex].x, skeleton.transform.position.y);
            skeleton.FlipTo(patrolTarget);
            // Ѳ�߷�Χ�ڷ���Ŀ��ʱ���뷴Ӧ״̬
            if (skeleton.target != null
                && skeleton.target.position.x > skeleton.chasePoints[0].x
                && skeleton.target.position.x < skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.React);
                return;
            }
            // ����Ѳ�ߵ����뾲ֹ״̬
            skeleton.transform.position = Vector2.MoveTowards(skeleton.transform.position,
                patrolTarget,
                skeleton.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(skeleton.transform.position, patrolTarget) < 0.1f)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Idle);
                return;
            }
        }

        public void OnExit()
        {
            // ����Ϊ��һ��Ѳ�ߵ�
            patrolIndex++;
            if (patrolIndex >= skeleton.patrolPoints.Length)
            {
                patrolIndex = 0;
            }
        }
    }

    public class ReactState : IState
    {
        private readonly Skeleton skeleton;
        public ReactState(Skeleton skeleton) => this.skeleton = skeleton;
        private AnimatorStateInfo animatorStateInfo;
        public void OnEnter()
        {
            Debug.Log("����������Ӧ��");
            skeleton._animator.Play("skeleton_react");
        }

        public void OnUpdate()
        {
            // �ܵ��˺�ʱ�����ܻ�״̬
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            animatorStateInfo = skeleton._animator.GetCurrentAnimatorStateInfo(0);
            // �������ſ����ʱ����׷��״̬
            if (animatorStateInfo.normalizedTime > 0.95f)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Chase);
                return;
            }
        }

        public void OnExit()
        {

        }
    }

    public class ChaseState : IState
    {
        private readonly Skeleton skeleton;
        public ChaseState(Skeleton skeleton) => this.skeleton = skeleton;
        public void OnEnter()
        {
            Debug.Log("��������׷�����");
            // ����׷���������˴����ƶ���������
            skeleton._animator.Play("skeleton_walk");
        }

        public void OnUpdate()
        {
            // �ܵ��˺�ʱ�����ܻ�״̬
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // Ŀ�����ʱ��Ŀ���ƶ�
            if (skeleton.target != null)
            {
                skeleton.FlipTo(skeleton.target.position);
                skeleton.transform.position = Vector2.MoveTowards(skeleton.transform.position,
                    new(skeleton.target.position.x, skeleton.transform.position.y),
                    skeleton.chaseSpeed * Time.deltaTime);
            }
            // Ŀ�궪ʧ�򳬳�׷����Χʱ���뾲ֹ״̬
            if (skeleton.target == null
                || skeleton.transform.position.x < skeleton.chasePoints[0].x
                || skeleton.transform.position.x > skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Idle);
                return;
            }
            // Ŀ����봥�������ķ�Χʱ���빥��״̬
            if (Physics2D.OverlapCircle(skeleton.attackPoint.position, skeleton.attackRadius, skeleton.targetLayer))
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Attack);
                return;
            }
        }

        public void OnExit()
        {

        }
    }

    public class AttackState : IState
    {
        private readonly Skeleton skeleton;
        public AttackState(Skeleton skeleton) => this.skeleton = skeleton;
        private AnimatorStateInfo animatorStateInfo;
        public void OnEnter()
        {
            Debug.Log("���÷���������");
            // ���Ź�������
            skeleton._animator.Play("skeleton_attack");
        }

        public void OnUpdate()
        {
            // �ܵ��˺�ʱ�����ܻ�״̬
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            animatorStateInfo = skeleton._animator.GetCurrentAnimatorStateInfo(0);
            // ���������ʱ����׷��״̬
            if (animatorStateInfo.normalizedTime > 0.95f)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Chase);
                return;
            }
        }

        public void OnExit()
        {

        }
    }

    public class HitState : IState
    {
        private readonly Skeleton skeleton;
        public HitState(Skeleton skeleton) => this.skeleton = skeleton;
        private AnimatorStateInfo animationStateInfo;
        public void OnEnter()
        {
            // �����ܻ�����
            skeleton._animator.Play("skeleton_hit");
        }

        public void OnUpdate()
        {
            // ����ֵ������0ʱ��������״̬
            if (skeleton.health <= 0)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Die);
                return;
            }
            // ���������ʱ����׷��״̬
            animationStateInfo = skeleton._animator.GetCurrentAnimatorStateInfo(0);
            if (animationStateInfo.normalizedTime > 0.95f)
            {
                if (skeleton.target == null)
                {
                    skeleton.target = GameObject.FindWithTag("Player").transform;
                }
                skeleton.fsm.TransitionState(SkeletonStateEnum.Chase);
                return;
            }
        }

        public void OnExit()
        {
            skeleton.isHit = false;
        }
    }

    public class DieState : IState
    {
        private readonly Skeleton skeleton;
        public DieState(Skeleton skeleton) => this.skeleton = skeleton;
        public void OnEnter()
        {
            skeleton._rigidbody.simulated = false;
            skeleton._animator.Play("skeleton_dead");
        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {

        }

        public void OnExit()
        {

        }
    }


}