using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("��Ҷ���")]
    public Player player;
    [Header("����ʱλ������")]
    public float attackMoveSpeed = 1f;
    [Header("ʱ���ڵ��ٴι�����Ϊ����")]
    public float attackComboTime = 1f;
    [Header("����ʱͣ��֡��")]
    public int attackPauseFrame = 10;
    [Header("�ɹ�����ͼ��")]
    public LayerMask attackLayer;

    private bool isAttack = false;  // �Ƿ��ڹ���
    private int attackCombo = 0;  // ��ǰ������
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
            if (timerAttackCombo <= 0f)  // ������ʱ������������
            {
                attackCombo = 0;
                isAttack = false;  // ͬʱˢ�¹���״̬�������򹥻���������϶�û�е���AttackOver���������޷��ٹ���
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

    // ʵ�ִ��ͣ��
    private IEnumerator AttackPause(int attackPauseFrame)
    {
        float pauseTime = attackPauseFrame / 60f;  // ��֡��ת������������Ϊ1��=60֡�����Գ���60
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

}