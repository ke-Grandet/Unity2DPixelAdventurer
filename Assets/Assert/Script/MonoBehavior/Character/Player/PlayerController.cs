using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动速率")]
    public float speed = 5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float horizontal = 0;  // 横轴输入

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rigidbody.simulated)
        {
            return;
        }

        //---- 获取横轴输入
        horizontal = Input.GetAxis("Horizontal");
        // 如果横轴输入为0
        if (Mathf.Approximately(horizontal, 0f))
        {
            // 切换为站立动画
            _animator.SetBool("Move", false);
        }
        // 如果横轴输入不为0
        else
        {
            // 切换为跑动动画
            _animator.SetBool("Move", true);
            // 动画朝右
            if (horizontal > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            // 动画朝左
            else
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_rigidbody.simulated)
        {
            return;
        }

        // 左右移动
        Move();
    }

    // 碰撞事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Saw"))
        {
            Hit();
        }
    }

    private void Move()
    {
        // 通过修改刚体速度实现左右移动，注意刚体和碰撞箱要使用无摩擦力的材质球
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = horizontal * speed;
        _rigidbody.velocity = velocity;
    }

    // 受击
    public void Hit()
    {
        // 取消刚体的物理模拟
        _rigidbody.simulated = false;
        // 播放战败动画，动画脚本中调用战败方法
        _animator.SetTrigger("Hit");
    }

    // 战败
    public void Defeated()
    {
        Destroy(gameObject);
        GameController.Instance.ShowGameOverPanel();
    }

}
