using Enum = System.Enum;
using Array = System.Array;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    [Header("移动速率")]
    [Range(1, 10)]
    public int speed = 4;
    [Header("最大生命值")]
    public int maxHealth = 10;
    [Header("战利品")]
    public GameObject booty;
    [Header("投掷物")]
    public GameObject missile;
    [Header("最短移动时间")]
    public float minMoveTime = 1f;
    [Header("最长移动时间")]
    public float maxMoveTime = 3f;

    [Header("墙壁图层")]
    [SerializeField]
    private LayerMask boundLayer;
    [Header("生命值UI")]
    [SerializeField]
    private HealthMask healthMask;

    private int direction = -1;  // 移动方向
    private float moveTime = 2f;  // 移动时间
    private int health;  // 当前生命值

    private bool isMoving = true;  // 是否正在移动

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

        // 碰到墙壁后掉头
        Vector2 rayOrigin = new(_rigidbody.position.x + direction * _capsuleCollider.bounds.size.x / 2,
            _rigidbody.position.y);
        RaycastHit2D raycastHit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, 0.1f, boundLayer);
        if (raycastHit.collider != null)
        {
            direction = -direction;
            _animator.SetFloat("Direction", direction);  // 播放移动动画
        }
        
        if (isMoving)
        {
            moveTime -= Time.deltaTime;
            if (moveTime <= 0)
            {
                if (Random.Range(0, 3) < 1)
                {
                    Act(0);  // 移动
                }
                else
                {
                    Act(1);  // 攻击
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
            // 如果玩家踩在头顶
            if (collision.GetContact(0).point.y >= _capsuleCollider.bounds.max.y - 0.2f)
            {
                // BOSS受击
                ChangeHealth(1);
                // 将玩家向上弹起
                Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
                playerBody.velocity = new(playerBody.velocity.x, 5f);
            }
            // 如果没有踩在头顶
            else
            {
                // 玩家受击
                player.Hit();
            }
        }
    }

    // 攻击
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

    // 改变生命值
    public void ChangeHealth(int damage)
    {
        health -= damage;
        float healthPercent = Mathf.Clamp01(health / (float)maxHealth);
        healthMask.ChangeValue(healthPercent);
        if (health <= 0)
        {
            _rigidbody.simulated = false;  // 取消刚体的物理模拟
            gameObject.GetComponentInChildren<ParticleSystem>().Play();  // 播放粒子特效
            GameController.Instance.GainScore(5000);  // 玩家获得分数
            _animator.SetInteger("Health", health);  // 播放战败动画，动画脚本中调用战败方法
        }
    }

    // 攻击之后的动作
    public void AfterAttack()
    {
        if (Random.Range(0, 3) < 1)
        {
            Act(0);  // 移动
        }
        else
        {
            Act(2);  // 继续攻击
        }
    }

    // 指定一个动作
    public void Act(int act)
    {
        switch (act)
        {
            // 移动
            case 0:
                isMoving = true;
                direction = -direction;  // 调转移动方向
                moveTime = Random.Range(minMoveTime, maxMoveTime);  // 刷新移动时间
                _animator.SetFloat("Direction", direction);  // 播放移动动画
                break;
            // 发动攻击
            case 1:
                isMoving = false;
                //direction = -direction;  // 调转移动方向
                _animator.SetTrigger("Attack");  // 播放攻击动画
                break;
            // 继续攻击
            case 2:
                direction = -direction;  // 调转移动方向
                _animator.SetTrigger("Keep Attack");  // 播放另一个攻击动画
                break;
            // 其它
            default:
                Debug.Log("BOSS动作出现意外数值");
                break;
        }
    }

    // 战败
    public void Defeated()
    {
        Booty();  // 掉落战利品
        Destroy(gameObject);  // 销毁自身
    }

    // 产生战利品
    public void Booty()
    {
        GameObject gameObject = Instantiate(booty, _rigidbody.position, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().velocity = new(0f, 5f);
    }

}
