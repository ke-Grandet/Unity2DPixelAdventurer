using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : AbstractEnemy
{

    [Header("移动速率")]
    public int speed = 3;
    [Header("跳跃速率")]
    public int jumpSpeed = 5;
    [Header("奖励分数")]
    public int score = 20;
    [Header("触地图层")]
    public LayerMask groundLayer;
    [Header("左巡逻点")]
    public Transform leftPoint;
    [Header("右巡逻点")]
    public Transform rightPoint;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    private Vector2 left;
    private Vector2 right;
    private int direction = -1;  // 朝向
    private bool isMove = false;  // 是否移动
    private bool isIdle = true;

    private readonly int anim_velocity_y = Animator.StringToHash("VelocityY");
    private readonly int anim_idle = Animator.StringToHash("Idle");
    private readonly int anim_die = Animator.StringToHash("Die");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        left = leftPoint.position;
        right = rightPoint.position;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    private void Update()
    {
        Debug.DrawRay(left, Vector2.up);
        Debug.DrawRay(right, Vector2.up);
        _animator.SetFloat(anim_velocity_y, _rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            _rigidbody.velocity = new(speed * direction, jumpSpeed);
            isMove = false;
        }
        if (isIdle)
        {
            _rigidbody.velocity = new(0, _rigidbody.velocity.y);
            isIdle = false;
        }
    }

    public void StartMove()
    {
        isMove = true;
        if (transform.position.x <= left.x)
        {
            direction = 1;
        }
        if (transform.position.x >= right.x)
        {
            direction = -1;
        }
        transform.localScale = new(-direction, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_boxCollider.IsTouchingLayers(groundLayer)
            && _animator.GetCurrentAnimatorStateInfo(0).IsName("frog_fall"))
        {
            isIdle = true;
            _animator.SetTrigger(anim_idle);
        }
    }

    public override void Hit(Transform source, int damage)
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        _animator.SetTrigger(anim_die);
        _rigidbody.simulated = false;
        GameManager.Instance.GainScore(score);
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
