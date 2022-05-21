using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float speed = 5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float horizontal = 0;  // ��������

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

        //---- ��ȡ��������
        horizontal = Input.GetAxis("Horizontal");
        // �����������Ϊ0
        if (Mathf.Approximately(horizontal, 0f))
        {
            // �л�Ϊվ������
            _animator.SetBool("Move", false);
        }
        // ����������벻Ϊ0
        else
        {
            // �л�Ϊ�ܶ�����
            _animator.SetBool("Move", true);
            // ��������
            if (horizontal > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            // ��������
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

        // �����ƶ�
        Move();
    }

    // ��ײ�¼�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Saw"))
        {
            Hit();
        }
    }

    private void Move()
    {
        // ͨ���޸ĸ����ٶ�ʵ�������ƶ���ע��������ײ��Ҫʹ����Ħ�����Ĳ�����
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = horizontal * speed;
        _rigidbody.velocity = velocity;
    }

    // �ܻ�
    public void Hit()
    {
        // ȡ�����������ģ��
        _rigidbody.simulated = false;
        // ����ս�ܶ����������ű��е���ս�ܷ���
        _animator.SetTrigger("Hit");
    }

    // ս��
    public void Defeated()
    {
        Destroy(gameObject);
        GameController.Instance.ShowGameOverPanel();
    }

}
