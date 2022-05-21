using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Íæ¼Ò¶ÔÏó")]
    public Player player;
    [Header("ÌøÔ¾ËÙÂÊ")]
    public int jumpSpeed = 5;
    [Header("ÌøÔ¾¶ÎÊý")]
    public int jumpTimes = 2;  // ¶ÎÊý2¼´Îª¶þ¶ÎÌø

    private bool jump = false;  // ÊÇ·ñÌøÔ¾
    private int jumpRemainingTimes;  //  Ê£ÓàÌøÔ¾¶ÎÊý


    // Start is called before the first frame update
    void Start()
    {
        jumpRemainingTimes = jumpTimes;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isCanAct)
        {
            return;
        }

        if (player.isOnGround)
        {
            jumpRemainingTimes = jumpTimes;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!player.isOnGround && jumpRemainingTimes == jumpTimes)
            {
                jumpRemainingTimes--;
            }
            if (jumpRemainingTimes > 0)
            {
                jump = true;
                player._animator.SetTrigger(player.anim_jump);
            }
        }
    }

    void FixedUpdate()
    {
        if (!player.isCanAct)
        {
            return;
        }

        if (jump)
        {
            player._rigidbody.velocity = new(player._rigidbody.velocity.x, jumpSpeed);
            jumpRemainingTimes--;
            jump = false;
        }
    }

}
