using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{

    public GameObject collectedEffect;  // ������ʧ��Ч
    public int score = 100;  // ����

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

    // ��ײ��ⷽ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�����
        if (collision.CompareTag("Player"))
        {
            // �����������Ⱦͼ�����ײ��
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;

            // ���õ�����ʧ��Ч
            if (collectedEffect != null)
            {
                collectedEffect.SetActive(true);
            }

            // ���ӷ���
            GameController.Instance.GainScore(score);

            // �ӳ�0.2�����������
            Destroy(gameObject, 0.2f);
        }
    }

}
