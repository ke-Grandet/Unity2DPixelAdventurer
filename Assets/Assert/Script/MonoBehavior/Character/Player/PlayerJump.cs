using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Header("跳跃力")]
    public int jumpForce = 5;
    [Header("跳跃段数")]
    public int jumpTimes = 2;  // 段数为2即二段跳
    [Header("可触地的图层")]
    public LayerMask groundLayer;  // 地面、平台等

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private bool jumpRequest = false;  // 跳跃键输入
    private int jumpRemainingTimes;  //  剩余跳跃段数
    private bool isOnGround = true;  // 是否触地

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        jumpRemainingTimes = jumpTimes;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rigidbody.simulated)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }

        // 根据纵轴速度切换起跳动画/下落动画
        float velocityY = _rigidbody.velocity.y;
        _animator.SetFloat("Velocity Y", Mathf.Approximately(velocityY, 0f) ? 0f : velocityY);

        // 触地检测，画一条从左脚到右脚的线段来与地板进行碰撞检测
        Vector2 rayOrigin = new(_rigidbody.position.x - _collider.bounds.size.x / 2 + 0.1f,
            _rigidbody.position.y - _spriteRenderer.bounds.size.y / 2);
        RaycastHit2D raycastHit = Physics2D.Raycast(
            rayOrigin, Vector2.right, _collider.bounds.size.x - 0.2f, groundLayer);

        // 画线便于观察
        Debug.DrawRay(rayOrigin,
            Vector2.right * _collider.bounds.size.x,
            raycastHit.collider != null ? Color.green : Color.red);


        // 更新触地变量
        isOnGround = raycastHit.collider != null;
        if (isOnGround)
        {
            jumpRemainingTimes = jumpTimes;
        }

        // 获取跳跃键输入
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
            if (!isOnGround && jumpRemainingTimes > 0)
            {
                // 切换二段跳动画
                _animator.SetTrigger("Double Jump");
            }
        }

    }

    void FixedUpdate()
    {
        if (!_rigidbody.simulated)
        {
            return;
        }

        // 跳跃
        if (jumpRequest)
        {
            // 如果未触地，且未跳跃过
            if (!isOnGround && jumpRemainingTimes == jumpTimes)
            {
                // 先消耗一次跳跃段数
                jumpRemainingTimes--;
            }
            if (jumpRemainingTimes > 0)
            {
                // 清零垂直速度
                _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
                // 加一个向上的力
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // 剩余跳跃段数减一
                jumpRemainingTimes--;
            }
            // 清除跳跃键输入
            jumpRequest = false;
        }
    }

}
