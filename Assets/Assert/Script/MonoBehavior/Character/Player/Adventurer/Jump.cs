using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("��Ҷ���")]
    public Player player;
    [Header("��Ծ����")]
    public int jumpSpeed = 5;
    [Header("��Ծ����")]
    public int jumpTimes = 2;  // ����2��Ϊ������

    private bool jump = false;  // �Ƿ���Ծ
    private int jumpRemainingTimes;  //  ʣ����Ծ����


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
