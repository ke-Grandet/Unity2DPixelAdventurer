using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Header("��Ծ��")]
    public int jumpForce = 5;
    [Header("��Ծ����")]
    public int jumpTimes = 2;  // ����Ϊ2��������
    [Header("�ɴ��ص�ͼ��")]
    public LayerMask groundLayer;  // ���桢ƽ̨��

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private bool jumpRequest = false;  // ��Ծ������
    private int jumpRemainingTimes;  //  ʣ����Ծ����
    private bool isOnGround = true;  // �Ƿ񴥵�

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

        // ���������ٶ��л���������/���䶯��
        float velocityY = _rigidbody.velocity.y;
        _animator.SetFloat("Velocity Y", Mathf.Approximately(velocityY, 0f) ? 0f : velocityY);

        // ���ؼ�⣬��һ������ŵ��ҽŵ��߶�����ذ������ײ���
        Vector2 rayOrigin = new(_rigidbody.position.x - _collider.bounds.size.x / 2 + 0.1f,
            _rigidbody.position.y - _spriteRenderer.bounds.size.y / 2);
        RaycastHit2D raycastHit = Physics2D.Raycast(
            rayOrigin, Vector2.right, _collider.bounds.size.x - 0.2f, groundLayer);

        // ���߱��ڹ۲�
        Debug.DrawRay(rayOrigin,
            Vector2.right * _collider.bounds.size.x,
            raycastHit.collider != null ? Color.green : Color.red);


        // ���´��ر���
        isOnGround = raycastHit.collider != null;
        if (isOnGround)
        {
            jumpRemainingTimes = jumpTimes;
        }

        // ��ȡ��Ծ������
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
            if (!isOnGround && jumpRemainingTimes > 0)
            {
                // �л�����������
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

        // ��Ծ
        if (jumpRequest)
        {
            // ���δ���أ���δ��Ծ��
            if (!isOnGround && jumpRemainingTimes == jumpTimes)
            {
                // ������һ����Ծ����
                jumpRemainingTimes--;
            }
            if (jumpRemainingTimes > 0)
            {
                // ���㴹ֱ�ٶ�
                _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
                // ��һ�����ϵ���
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // ʣ����Ծ������һ
                jumpRemainingTimes--;
            }
            // �����Ծ������
            jumpRequest = false;
        }
    }

}
