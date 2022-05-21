using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{

    public GameObject collectedEffect;  // 道具消失特效
    public int score = 100;  // 分数

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 碰撞检测方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞到玩家
        if (collision.CompareTag("Player"))
        {
            // 禁用自身的渲染图像和碰撞箱
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;

            // 启用道具消失特效
            if (collectedEffect != null)
            {
                collectedEffect.SetActive(true);
            }

            // 增加分数
            GameController.Instance.GainScore(score);

            // 延迟0.2秒后销毁自身
            Destroy(gameObject, 0.2f);
        }
    }

}
