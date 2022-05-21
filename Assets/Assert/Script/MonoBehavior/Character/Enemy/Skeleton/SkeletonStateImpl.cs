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
            Debug.Log("骷髅静止不动");
            // 播放静止动画
            skeleton._animator.Play("skeleton_idle");
        }

        public void OnUpdate()
        {
            // 受到伤害时进入受击状态
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // 追击范围内发现目标时进入反应状态
            if (skeleton.target != null
                && skeleton.target.position.x > skeleton.chasePoints[0].x
                && skeleton.target.position.x < skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.React);
                return;
            }
            // 计时器超过静止时间时进入移动状态
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
            Debug.Log("骷髅正在巡逻");
            // 播放巡逻动画
            skeleton._animator.Play("skeleton_walk");
        }

        public void OnUpdate()
        {
            // 受到伤害时进入受击状态
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // 根据当前巡逻点确定移动方向
            patrolTarget = new(skeleton.patrolPoints[patrolIndex].x, skeleton.transform.position.y);
            skeleton.FlipTo(patrolTarget);
            // 巡逻范围内发现目标时进入反应状态
            if (skeleton.target != null
                && skeleton.target.position.x > skeleton.chasePoints[0].x
                && skeleton.target.position.x < skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.React);
                return;
            }
            // 到达巡逻点后进入静止状态
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
            // 更换为下一个巡逻点
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
            Debug.Log("骷髅做出反应了");
            skeleton._animator.Play("skeleton_react");
        }

        public void OnUpdate()
        {
            // 受到伤害时进入受击状态
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            animatorStateInfo = skeleton._animator.GetCurrentAnimatorStateInfo(0);
            // 动画播放快结束时进入追击状态
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
            Debug.Log("骷髅正在追击玩家");
            // 播放追击动画，此处用移动动画代替
            skeleton._animator.Play("skeleton_walk");
        }

        public void OnUpdate()
        {
            // 受到伤害时进入受击状态
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            // 目标存在时向目标移动
            if (skeleton.target != null)
            {
                skeleton.FlipTo(skeleton.target.position);
                skeleton.transform.position = Vector2.MoveTowards(skeleton.transform.position,
                    new(skeleton.target.position.x, skeleton.transform.position.y),
                    skeleton.chaseSpeed * Time.deltaTime);
            }
            // 目标丢失或超出追击范围时进入静止状态
            if (skeleton.target == null
                || skeleton.transform.position.x < skeleton.chasePoints[0].x
                || skeleton.transform.position.x > skeleton.chasePoints[1].x)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Idle);
                return;
            }
            // 目标进入触发攻击的范围时进入攻击状态
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
            Debug.Log("骷髅发动攻击！");
            // 播放攻击动画
            skeleton._animator.Play("skeleton_attack");
        }

        public void OnUpdate()
        {
            // 受到伤害时进入受击状态
            if (skeleton.isHit)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Hit);
                return;
            }
            animatorStateInfo = skeleton._animator.GetCurrentAnimatorStateInfo(0);
            // 动画快结束时进入追击状态
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
            // 播放受击动画
            skeleton._animator.Play("skeleton_hit");
        }

        public void OnUpdate()
        {
            // 生命值不大于0时进入死亡状态
            if (skeleton.health <= 0)
            {
                skeleton.fsm.TransitionState(SkeletonStateEnum.Die);
                return;
            }
            // 动画快结束时进入追击状态
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