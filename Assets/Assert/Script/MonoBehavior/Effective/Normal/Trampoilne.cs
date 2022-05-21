using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoilne : MonoBehaviour
{

    public float jumpForce = 22;  // 弹力大小

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 触发动画
            _animator.SetTrigger("jump");
            // 将碰撞物弹起：通过调用碰撞物的刚体组件施加向上的力来实现
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

}
