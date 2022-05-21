using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    public BoxCollider2D _boxCollider;
    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public SpriteRenderer _spriteRenderer;
    [HideInInspector]
    public float lookDirection = 1;  // ��ҳ���
    [HideInInspector]
    public bool isOnGround = true;  // �Ƿ񴥵�
    [HideInInspector]
    public bool isCanAct = true;  // �Ƿ����ж�

    [Header("����ֵ����")]
    public int maxHealth = 100;
    [Header("�ƶ�����")]
    public int moveSpeed = 10;
    [Header("�ܻ��޵�ʱ��")]
    public float invincibleTime = 1f;
    [Header("�ܻ��˺�����")]
    public int backSpeed = 50;
    [Header("����ͼ��")]
    public LayerMask groundLayer;
    [Header("�����ж�ƫ��")]
    public float groundOffset = 0.018f;

    // ����������
    public readonly int anim_isOnGround = Animator.StringToHash("IsOnGround");
    public readonly int anim_jump = Animator.StringToHash("Jump");
    public readonly int anim_horizontal = Animator.StringToHash("Horizontal");
    public readonly int anim_vertical = Animator.StringToHash("VelocityY");
    public readonly int anim_attack = Animator.StringToHash("Attack");
    public readonly int anim_hurt = Animator.StringToHash("Hurt");
    public readonly int anim_die = Animator.StringToHash("Die");
    public readonly int anim_attackCombo = Animator.StringToHash("AttackCombo");

    private float horizontal = 0f;
    private float timerInvincible = 0f;
    private int backDirection = 0;  // �ܻ�ʱ���˵ķ���
    private int health;
   
    public int Health { get { return health; } }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update()
    {
        if (!isCanAct)
        {
            return;
        }

        // ˢ����ҳ���
        if (lookDirection * Input.GetAxisRaw("Horizontal") < 0)  // ���Ϊ��˵�������෴
        {
            lookDirection = -lookDirection;
            transform.localScale = new(lookDirection, transform.localScale.y, transform.localScale.z);
        }

        // ˢ�º�������
        horizontal = Input.GetAxis("Horizontal");
        _animator.SetFloat(anim_horizontal, horizontal);
        _animator.SetFloat(anim_vertical, _rigidbody.velocity.y);

        // ˢ���ܻ��޵�ʱ��
        if (timerInvincible > 0f)
        {
            timerInvincible -= Time.deltaTime;
        }

        // ���ؼ��
        Vector2 rayOrigin = new(_rigidbody.position.x - _boxCollider.bounds.size.x * 0.5f,
            _rigidbody.position.y - _spriteRenderer.bounds.size.y * 0.5f - groundOffset);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, _boxCollider.bounds.size.x, groundLayer);
        if (hit.collider != null)
        {
            isOnGround = true;
            _animator.SetBool(anim_isOnGround, true);
        }
        else
        {
            isOnGround = false;
            _animator.SetBool(anim_isOnGround, false);
        }
        Debug.DrawRay(rayOrigin, Vector2.right * _boxCollider.bounds.size.x, hit.collider != null ? Color.green : Color.red);

    }

    private void FixedUpdate()
    {
        if (!isCanAct)
        {
            return;
        }

        if (backDirection != 0)
        {
            _rigidbody.velocity = new(backDirection * backSpeed, _rigidbody.velocity.y);
            backDirection = 0;
        }
        else
        {
            _rigidbody.velocity = new(horizontal * moveSpeed, _rigidbody.velocity.y);
        }
    }

    public void Heal(Transform source, int healValue)
    {
        Debug.Log("�ָ�����ֵ��" + healValue);
        health += healValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log("��ǰ����ֵ��" + health);
        GameManager.Instance.UpdatePlayerHealthUI(health / (float)maxHealth);
    }

    public void Hit(Transform source, int damage)
    {
        if (timerInvincible > 0f)
        {
            return;
        }
        health -= damage;
        GameManager.Instance.UpdatePlayerHealthUI(health / (float)maxHealth);
        if (source.position.x > transform.position.x)
        {
            backDirection = -1;
        }
        else
        {
            backDirection = 1;
        }
        if (health <= 0)
        {
            _animator.SetTrigger(anim_die);
            isCanAct = false;
            _rigidbody.simulated = false;
        }
        else
        {
            _animator.SetTrigger(anim_hurt);
            timerInvincible = invincibleTime;
        }
    }

    private void OnDrawGizmos()
    {
        // �������ؼ�����
        //Vector2 rayOrigin = new(_rigidbody.position.x - _boxCollider.bounds.size.x * 0.5f,
        //    _rigidbody.position.y - _spriteRenderer.bounds.size.y * 0.5f);
        //Gizmos.color = isOnGround ? Color.green : Color.red;
        //Gizmos.DrawRay(rayOrigin, Vector2.right * _spriteRenderer.bounds.size.y);
    }

}
