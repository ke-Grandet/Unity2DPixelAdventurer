using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrog : MonoBehaviour
{
    public int score = 1000;
    public float speed = 2f;
    public LayerMask layer;  // �ϰ���ͼ�㼯��

    public Transform headPoint;  // ͷ��λ��
    public Transform rightUp;  // ǰ�Ϸ�λ��
    public Transform rightDown;  // ǰ�·�λ��

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

        // �ж���ײ
        _collided = Physics2D.Linecast(rightUp.position, rightDown.position, layer);

        // �����ײ���ϰ���
        if (_collided)
        {
            // �������߱��ڹ۲�
            Debug.DrawLine(rightUp.position, rightDown.position, Color.red);
            // ��ת����
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
            // �ж�����ҵ���ײλ��
            bool overHead = collision.contacts[0].point.y > headPoint.position.y ? true : false;

            // �����ײλ�ø���ͷ��
            if (overHead)
            {
                // ����ҵ���
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                // �ٶȸ�Ϊ0
                speed = 0;
                // ������������
                _animator.SetTrigger("die");
                // ������ײ��
                _capsuleCollider2D.enabled = false;
                // �������͸�Ϊ�̶������������ײ�������������������ذ�
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                // ��������
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
