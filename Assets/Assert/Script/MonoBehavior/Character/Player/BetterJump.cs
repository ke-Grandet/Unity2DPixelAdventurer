using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public float fallMultiplier = 2.5f;  // ����ʱ����������
    public float lowJumpMultiplier = 2f;  // ����ʱ����������

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // ��������ٶȵ�����ֵС��0����ʾ��������
        if (_rigidbody2D.velocity.y < 0)
        {
            // �ı�����������ʹ����ӿ�һЩ
            _rigidbody2D.gravityScale = fallMultiplier;
        }
        // ��������ٶȵ�����ֵ����0����δ��ס��Ծ������ʾ����
        else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // �ı�����������ʹ����ӿ�һ��
            _rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        // �����������ʾ��������
        else
        {
            // ������������
            _rigidbody2D.gravityScale = 1f;
        }
    }

}
