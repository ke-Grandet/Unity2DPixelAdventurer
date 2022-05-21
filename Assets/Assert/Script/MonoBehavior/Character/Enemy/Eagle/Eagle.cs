using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : AbstractEnemy
{

    [Header("飞行速率")]
    public int speed = 3;
    [Header("追击速率")]
    public int chaseSpeed = 5;
    [Header("奖励分数")]
    public int score = 20;
    [Header("巡逻点1")]
    public Transform patrolPoint1;
    [Header("巡逻点2")]
    public Transform patrolPoint2;
    [Header("攻击点1")]
    public Transform attackPoint1;
    [Header("攻击点2")]
    public Transform attackPoint2;

    private Vector2[] patrolPointArr = new Vector2[2];
    private Vector2 attackLineStart;
    private Vector2 attackLineEnd;
    private int pointIndex = 0;
    private Transform target = null;
    private bool isDead = false;
    private bool isAttack = false;

    private void Start()
    {
        patrolPointArr[0] = patrolPoint1.position;
        patrolPointArr[1] = patrolPoint2.position;
        Destroy(patrolPoint1.gameObject);
        Destroy(patrolPoint2.gameObject);
        attackLineStart = attackPoint1.position;
        attackLineEnd = attackPoint2.position;
        Destroy(attackPoint1.gameObject);
        Destroy(attackPoint2.gameObject);
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        Debug.DrawLine(attackLineStart, attackLineEnd);
        if (isAttack)
        {
            if (target == null)
            {
                isAttack = false;
            }
            else
            {
                FlipTo(target.position);
                transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, target.position) <= 0.1f
                    || target.position.x < attackLineStart.x
                    || target.position.x > attackLineEnd.x)
                {
                    isAttack = false;
                }
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Linecast(attackLineStart, attackLineEnd, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                target = hit.collider.transform;
                isAttack = true;
            }
            else
            {
                target = null;
                FlipTo(patrolPointArr[pointIndex]);
                transform.position = Vector2.MoveTowards(transform.position, patrolPointArr[pointIndex], speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPointArr[pointIndex]) <= 0.1f)
                {
                    pointIndex++;
                    if (pointIndex >= patrolPointArr.Length)
                    {
                        pointIndex = 0;
                    }
                }
            }
        }
    }

    private void FlipTo(Vector2 target)
    {
        if (transform.position.x > target.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public override void Hit(Transform source, int damage)
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
        GameManager.Instance.GainScore(score);
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
