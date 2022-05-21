using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkeletonSpace;
public class Skeleton : AbstractEnemy
{
    [HideInInspector]
    public FSM<SkeletonStateEnum> fsm;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public BoxCollider2D _boxCollider;
    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public Vector2[] patrolPoints = new Vector2[2];
    [HideInInspector]
    public Vector2[] chasePoints = new Vector2[2];
    [HideInInspector]
    public Transform target;  // ����Ŀ��

    [Header("����ֵ")]
    public int health = 3;
    [Header("��ֹʱ��")]
    public float idleTime = 1f;
    [Header("Ѳ��ʱ���ƶ�����")]
    public int moveSpeed = 5;
    [Header("׷��ʱ���ƶ�����")]
    public int chaseSpeed = 10;
    [Header("Ѳ�߷�Χ")]
    public Transform[] patrolRange = new Transform[2];
    [Header("׷����Χ")]
    public Transform[] chaseRange = new Transform[2];
    [Header("����Ŀ���ͼ��")]
    public LayerMask targetLayer;
    [Header("����������Բ��")]
    public Transform attackPoint;
    [Header("���������İ뾶")]
    public float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        patrolPoints[0] = patrolRange[0].position;
        patrolPoints[1] = patrolRange[1].position;
        Destroy(patrolRange[0].gameObject);
        Destroy(patrolRange[1].gameObject);
        chasePoints[0] = chaseRange[0].position;
        chasePoints[1] = chaseRange[1].position;
        Destroy(chaseRange[0].gameObject);
        Destroy(chaseRange[1].gameObject);

        fsm = BuildFSM();
    }

    private FSM<SkeletonStateEnum> BuildFSM()
    {
        Dictionary<SkeletonStateEnum, IState> states = new();
        states.Add(SkeletonStateEnum.Idle, new IdleState(this));
        states.Add(SkeletonStateEnum.Patrol, new PatrolState(this));
        states.Add(SkeletonStateEnum.React, new ReactState(this));
        states.Add(SkeletonStateEnum.Chase, new ChaseState(this));
        states.Add(SkeletonStateEnum.Attack, new AttackState(this));
        states.Add(SkeletonStateEnum.Hit, new HitState(this));
        states.Add(SkeletonStateEnum.Die, new DieState(this));
        return new FSM<SkeletonStateEnum>(states, SkeletonStateEnum.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdate();
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("���÷���Ŀ�꣡");
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("���ö�ʧĿ�ꡣ����");
            target = null;
        }
    }

    public override void Hit(Transform source, int damage)
    {
        if (!isHit)
        {
            isHit = true;
            health -= damage;
            target = source;
            FlipTo(target.position);
        }
    }

    public void FlipTo(Vector2 target)
    {
        if (target == null)
        {
            return;
        }
        if (target.x > transform.position.x)
        {
            transform.localScale = new(1, 1, 1);
        }
        else
        {
            transform.localScale = new(-1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        // �������������ķ�Χ
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        // ����Ѳ�߷�Χ
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(patrolPoints[0], new(patrolPoints[0].x, 1));
        Gizmos.DrawLine(patrolPoints[1], new(patrolPoints[1].x, 1));
        // ����׷����Χ
        Gizmos.color = Color.red;
        Gizmos.DrawLine(chasePoints[0], new(chasePoints[0].x, 1));
        Gizmos.DrawLine(chasePoints[1], new(chasePoints[1].x, 1));
    }

}
