using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float _timer;  // 下落延时
    private const float RockHeadWaitingTimeSpan = 2f;  // 落地后滞留时间
    private const float RockHeadMoveBackTimeSpan = 0.4f;  // 返回用时

    private bool _isGrounded = false;  // 是否着地
    private Vector3 _originalPosition;  // 初始位置
    private Vector3 _velocity = Vector3.zero;  // 速度，无实际作用

    private static readonly int IsBottomHit = Animator.StringToHash("isBottomHit");

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _originalPosition = _rigidbody2D.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 如果着地
        if (_isGrounded)
        {
            _timer += Time.fixedDeltaTime;

            // 着地时间达到限制，且物体不在原位
            if (_timer >= RockHeadWaitingTimeSpan && transform.position != _originalPosition)
            {
                // 重置刚体参数
                _rigidbody2D.isKinematic = true;
                _rigidbody2D.gravityScale = 1f;
                // 平滑移动
                transform.position = Vector3.SmoothDamp(
                    _rigidbody2D.position, _originalPosition, ref _velocity, RockHeadMoveBackTimeSpan);
            }
            else if (transform.position == _originalPosition)
            {
                _isGrounded = false;
                _timer = 0;
            }
        }
    }

    // 触发器检测
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 当触发者是玩家时
        if (collision.gameObject.CompareTag("Player"))
        {
            // 取消刚体的固定类型，使其下落
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.gravityScale = 2f;
        }
    }

    // 碰撞箱检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰到地面
        if (collision.gameObject.layer == 8)
        {
            // 着地标识改为真
            _isGrounded = true;
            // 播放动画
            _animator.SetTrigger(IsBottomHit);
        }

        // 如果碰到玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 调用玩家受击方法
            collision.gameObject.GetComponent<PlayerController>().Hit();
            //// 修改玩家图像高度
            //Vector3 newLocalScale = collision.gameObject.transform.localScale;
            //collision.gameObject.transform.localScale = new Vector3(newLocalScale.x, newLocalScale.y * 0.1f);

            //// 修改玩家碰撞箱高度
            //Vector2 newSize = collision.gameObject.GetComponent<CapsuleCollider2D>().size;
            //collision.gameObject.GetComponent<CapsuleCollider2D>().size = 
            //    new Vector2(newSize.x * 0.1f, newSize.y * 0.1f);

            //// 销毁玩家
            //Destroy(collision.gameObject, 0.5f);
            //Invoke(nameof(ShowGameOverPanel), 0.5f);
        }
    }

    private void ShowGameOverPanel()
    {
        GameController.Instance.ShowGameOverPanel();
    }

}
