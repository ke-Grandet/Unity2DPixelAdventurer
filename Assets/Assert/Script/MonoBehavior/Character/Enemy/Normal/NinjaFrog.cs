using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrog : MonoBehaviour
{
    public int score = 1000;
    public float speed = 2f;
    public LayerMask layer;  // 障碍物图层集合

    public Transform headPoint;  // 头部位置
    public Transform rightUp;  // 前上方位置
    public Transform rightDown;  // 前下方位置

    private bool _collided = false;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(speed, _rigidbody2D.velocity.y, 0) * Time.deltaTime;
        transform.position += movement;

        // 判断碰撞
        _collided = Physics2D.Linecast(rightUp.position, rightDown.position, layer);

        // 如果碰撞到障碍物
        if (_collided)
        {
            // 画出红线便于观察
            Debug.DrawLine(rightUp.position, rightDown.position, Color.red);
            // 调转方向
            speed = -speed;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            Debug.DrawLine(rightUp.position, rightDown.position, Color.green);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 判断与玩家的碰撞位置
            bool overHead = collision.contacts[0].point.y > headPoint.position.y ? true : false;

            // 如果碰撞位置高于头部
            if (overHead)
            {
                // 让玩家弹起
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                // 速度改为0
                speed = 0;
                // 触发死亡动画
                _animator.SetTrigger("die");
                // 禁用碰撞箱
                _capsuleCollider2D.enabled = false;
                // 刚体类型改为固定，避免禁用碰撞箱后因重力下落而穿过地板
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                // 销毁自身
                Destroy(gameObject, 1.33f);
                GameController.Instance.GainScore(score);
            }
            else
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Hit();
                }
            }
        }
    }

}
