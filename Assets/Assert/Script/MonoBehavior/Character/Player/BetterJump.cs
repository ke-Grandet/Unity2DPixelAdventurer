using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public float fallMultiplier = 2.5f;  // 下落时的重力倍数
    public float lowJumpMultiplier = 2f;  // 短跳时的重力倍数

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
        // 如果刚体速度的纵向值小于0，表示正在下落
        if (_rigidbody2D.velocity.y < 0)
        {
            // 改变重力倍数，使下落加快一些
            _rigidbody2D.gravityScale = fallMultiplier;
        }
        // 如果刚体速度的纵向值大于0，且未按住跳跃键，表示短跳
        else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // 改变重力倍数，使下落加快一点
            _rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        // 其它情况，表示正在上升
        else
        {
            // 重置重力倍数
            _rigidbody2D.gravityScale = 1f;
        }
    }

}
