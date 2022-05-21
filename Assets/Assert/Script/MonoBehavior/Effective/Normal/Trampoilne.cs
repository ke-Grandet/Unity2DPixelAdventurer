using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoilne : MonoBehaviour
{

    public float jumpForce = 22;  // ������С

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
            // ��������
            _animator.SetTrigger("jump");
            // ����ײ�ﵯ��ͨ��������ײ��ĸ������ʩ�����ϵ�����ʵ��
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

}
