using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("玩家对象")]
    public Player player;
    [Header("攻击时位移速率")]
    public float attackMoveSpeed = 1f;
    [Header("时间内的再次攻击计为连击")]
    public float attackComboTime = 1f;
    [Header("攻击时停顿帧数")]
    public int attackPauseFrame = 10;
    [Header("可攻击的图层")]
    public LayerMask attackLayer;

    private bool isAttack = false;  // 是否在攻击
    private int attackCombo = 0;  // 当前连击数
    private float timerAttackCombo = 0f;

    private void Update()
    {
        if (!player.isCanAct)
        {
            return;
        }

        if (timerAttackCombo > 0f)
        {
            timerAttackCombo -= Time.deltaTime;
            if (timerAttackCombo <= 0f)  // 连击超时则将连击数置零
            {
                attackCombo = 0;
                isAttack = false;  // 同时刷新攻击状态，以免因攻击动画被打断而没有调用AttackOver方法导致无法再攻击
            }
        }

        if (Input.GetButtonDown("Attack") && !isAttack)
        {
            isAttack = true;
            attackCombo++;
            if (attackCombo > 3)
            {
                attackCombo = 1;
            }
            timerAttackCombo = attackComboTime;
            player._animator.SetTrigger(player.anim_attack);
            player._animator.SetInteger(player.anim_attackCombo, attackCombo);
        }
    }

    private void FixedUpdate()
    {
        if (!player.isCanAct)
        {
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBeHit enemyHurt = collision.GetComponent<EnemyBeHit>();
        if (enemyHurt != null)
        {
            enemyHurt.Hit(transform, attackCombo);
            StartCoroutine(AttackPause(attackPauseFrame));
        }
    }

    public void AttackOver()
    {
        isAttack = false;
    }

    // 实现打击停顿
    private IEnumerator AttackPause(int attackPauseFrame)
    {
        float pauseTime = attackPauseFrame / 60f;  // 将帧数转换成秒数，因为1秒=60帧，所以除以60
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

}