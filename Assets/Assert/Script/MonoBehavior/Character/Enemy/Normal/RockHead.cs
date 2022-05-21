using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float _timer;  // ������ʱ
    private const float RockHeadWaitingTimeSpan = 2f;  // ��غ�����ʱ��
    private const float RockHeadMoveBackTimeSpan = 0.4f;  // ������ʱ

    private bool _isGrounded = false;  // �Ƿ��ŵ�
    private Vector3 _originalPosition;  // ��ʼλ��
    private Vector3 _velocity = Vector3.zero;  // �ٶȣ���ʵ������

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
        // ����ŵ�
        if (_isGrounded)
        {
            _timer += Time.fixedDeltaTime;

            // �ŵ�ʱ��ﵽ���ƣ������岻��ԭλ
            if (_timer >= RockHeadWaitingTimeSpan && transform.position != _originalPosition)
            {
                // ���ø������
                _rigidbody2D.isKinematic = true;
                _rigidbody2D.gravityScale = 1f;
                // ƽ���ƶ�
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

    // ���������
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �������������ʱ
        if (collision.gameObject.CompareTag("Player"))
        {
            // ȡ������Ĺ̶����ͣ�ʹ������
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.gravityScale = 2f;
        }
    }

    // ��ײ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����������
        if (collision.gameObject.layer == 8)
        {
            // �ŵر�ʶ��Ϊ��
            _isGrounded = true;
            // ���Ŷ���
            _animator.SetTrigger(IsBottomHit);
        }

        // ����������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��������ܻ�����
            collision.gameObject.GetComponent<PlayerController>().Hit();
            //// �޸����ͼ��߶�
            //Vector3 newLocalScale = collision.gameObject.transform.localScale;
            //collision.gameObject.transform.localScale = new Vector3(newLocalScale.x, newLocalScale.y * 0.1f);

            //// �޸������ײ��߶�
            //Vector2 newSize = collision.gameObject.GetComponent<CapsuleCollider2D>().size;
            //collision.gameObject.GetComponent<CapsuleCollider2D>().size = 
            //    new Vector2(newSize.x * 0.1f, newSize.y * 0.1f);

            //// �������
            //Destroy(collision.gameObject, 0.5f);
            //Invoke(nameof(ShowGameOverPanel), 0.5f);
        }
    }

    private void ShowGameOverPanel()
    {
        GameController.Instance.ShowGameOverPanel();
    }

}
