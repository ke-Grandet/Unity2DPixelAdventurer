using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float fallingTime = 2;  // �����ӳ�

    private TargetJoint2D _targetJoint2D;
    private BoxCollider2D _boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _targetJoint2D = GetComponent<TargetJoint2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ָ��ʱ��������Ŀ�귽��
            Invoke(nameof(Falling), fallingTime);
        }
        // ����ǵش�
        if (collision.gameObject.CompareTag("Spike"))
        {
            // �ݻ�����
            Destroy(gameObject);
        }
    }

    private void Falling()
    {
        // ����joint�����������ڸ������������������Ӱ�������
        _targetJoint2D.enabled = false;
    }

}
