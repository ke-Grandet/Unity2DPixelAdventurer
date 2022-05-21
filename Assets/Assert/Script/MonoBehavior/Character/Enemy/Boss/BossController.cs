using Enum = System.Enum;
using Array = System.Array;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    [Header("�ƶ�����")]
    [Range(1, 10)]
    public int speed = 4;
    [Header("�������ֵ")]
    public int maxHealth = 10;
    [Header("ս��Ʒ")]
    public GameObject booty;
    [Header("Ͷ����")]
    public GameObject missile;
    [Header("����ƶ�ʱ��")]
    public float minMoveTime = 1f;
    [Header("��ƶ�ʱ��")]
    public float maxMoveTime = 3f;

    [Header("ǽ��ͼ��")]
    [SerializeField]
    private LayerMask boundLayer;
    [Header("����ֵUI")]
    [SerializeField]
    private HealthMask healthMask;

    private int direction = -1;  // �ƶ�����
    private float moveTime = 2f;  // �ƶ�ʱ��
    private int health;  // ��ǰ����ֵ

    private bool isMoving = true;  // �Ƿ������ƶ�

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            return;
        }

        // ����ǽ�ں��ͷ
        Vector2 rayOrigin = new(_rigidbody.position.x + direction * _capsuleCollider.bounds.size.x / 2,
            _rigidbody.position.y);
        RaycastHit2D raycastHit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, 0.1f, boundLayer);
        if (raycastHit.collider != null)
        {
            direction = -direction;
            _animator.SetFloat("Direction", direction);  // �����ƶ�����
        }
        
        if (isMoving)
        {
            moveTime -= Time.deltaTime;
            if (moveTime <= 0)
            {
                if (Random.Range(0, 3) < 1)
                {
                    Act(0);  // �ƶ�
                }
                else
                {
                    Act(1);  // ����
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            return;
        }

        if (isMoving)
        {
            _rigidbody.velocity = new(direction * speed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new(0f, _rigidbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            // �����Ҳ���ͷ��
            if (collision.GetContact(0).point.y >= _capsuleCollider.bounds.max.y - 0.2f)
            {
                // BOSS�ܻ�
                ChangeHealth(1);
                // ��������ϵ���
                Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
                playerBody.velocity = new(playerBody.velocity.x, 5f);
            }
            // ���û�в���ͷ��
            else
            {
                // ����ܻ�
                player.Hit();
            }
        }
    }

    // ����
    public void Attack()
    {
        GameObject redBomb = Instantiate(
            missile,
            new(_rigidbody.position.x + _spriteRenderer.bounds.size.x / 2 * direction,
                _rigidbody.position.y + _spriteRenderer.bounds.size.y, 0f),
            Quaternion.identity
        );
        redBomb.GetComponent<Rigidbody2D>().velocity = new(Random.Range(3f, 5f) * direction, 5f);
}

    // �ı�����ֵ
    public void ChangeHealth(int damage)
    {
        health -= damage;
        float healthPercent = Mathf.Clamp01(health / (float)maxHealth);
        healthMask.ChangeValue(healthPercent);
        if (health <= 0)
        {
            _rigidbody.simulated = false;  // ȡ�����������ģ��
            gameObject.GetComponentInChildren<ParticleSystem>().Play();  // ����������Ч
            GameController.Instance.GainScore(5000);  // ��һ�÷���
            _animator.SetInteger("Health", health);  // ����ս�ܶ����������ű��е���ս�ܷ���
        }
    }

    // ����֮��Ķ���
    public void AfterAttack()
    {
        if (Random.Range(0, 3) < 1)
        {
            Act(0);  // �ƶ�
        }
        else
        {
            Act(2);  // ��������
        }
    }

    // ָ��һ������
    public void Act(int act)
    {
        switch (act)
        {
            // �ƶ�
            case 0:
                isMoving = true;
                direction = -direction;  // ��ת�ƶ�����
                moveTime = Random.Range(minMoveTime, maxMoveTime);  // ˢ���ƶ�ʱ��
                _animator.SetFloat("Direction", direction);  // �����ƶ�����
                break;
            // ��������
            case 1:
                isMoving = false;
                //direction = -direction;  // ��ת�ƶ�����
                _animator.SetTrigger("Attack");  // ���Ź�������
                break;
            // ��������
            case 2:
                direction = -direction;  // ��ת�ƶ�����
                _animator.SetTrigger("Keep Attack");  // ������һ����������
                break;
            // ����
            default:
                Debug.Log("BOSS��������������ֵ");
                break;
        }
    }

    // ս��
    public void Defeated()
    {
        Booty();  // ����ս��Ʒ
        Destroy(gameObject);  // ��������
    }

    // ����ս��Ʒ
    public void Booty()
    {
        GameObject gameObject = Instantiate(booty, _rigidbody.position, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().velocity = new(0f, 5f);
    }

}
