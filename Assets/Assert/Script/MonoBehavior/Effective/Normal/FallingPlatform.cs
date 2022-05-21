using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float fallingTime = 2;  // 下落延迟

    private TargetJoint2D _targetJoint2D;
    private BoxCollider2D _boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _targetJoint2D = GetComponent<TargetJoint2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 在指定时间过后调用目标方法
            Invoke(nameof(Falling), fallingTime);
        }
        // 如果是地刺
        if (collision.gameObject.CompareTag("Spike"))
        {
            // 摧毁自身
            Destroy(gameObject);
        }
    }

    private void Falling()
    {
        // 禁用joint组件后，物体会在刚体组件作用下受重力影响而下落
        _targetJoint2D.enabled = false;
    }

}
