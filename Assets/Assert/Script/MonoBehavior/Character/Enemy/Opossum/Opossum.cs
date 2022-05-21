using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : AbstractEnemy
{

    [Header("�ƶ�����")]
    public int speed = 5;
    [Header("������")]
    public int attack = 1;
    [Header("��������")]
    public int score = 10;
    [Header("�Ƿ�˳ʱ��Ѳ��")]
    public bool isClockwire = false;
    [Header("����Ѳ�ߵ�")]
    public Transform leftTop;
    [Header("����Ѳ�ߵ�")]
    public Transform leftBottom;
    [Header("����Ѳ�ߵ�")]
    public Transform rightBottom;
    [Header("����Ѳ�ߵ�")]
    public Transform rightTop;

    private readonly Vector2[] pointArr = new Vector2[4];
    private int pointIndex = 0;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isClockwire)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            pointArr[0] = rightTop.position;
            pointArr[1] = rightBottom.position;
            pointArr[2] = leftBottom.position;
            pointArr[3] = leftTop.position;
        }
        else
        {
            pointArr[0] = leftTop.position;
            pointArr[1] = leftBottom.position;
            pointArr[2] = rightBottom.position;
            pointArr[3] = rightTop.position;
        }
        Destroy(leftTop.gameObject);
        Destroy(leftBottom.gameObject);
        Destroy(rightTop.gameObject);
        Destroy(rightBottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        Debug.DrawLine(pointArr[0], pointArr[1]);
        Debug.DrawLine(pointArr[1], pointArr[2]);
        Debug.DrawLine(pointArr[2], pointArr[3]);
        Debug.DrawLine(pointArr[3], pointArr[0]);

        transform.position = Vector2.MoveTowards(transform.position, pointArr[pointIndex], speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, pointArr[pointIndex]) < 0.1f)
        {
            pointIndex++;
            if (pointIndex >= pointArr.Length)
            {
                pointIndex = 0;
            }
            transform.RotateAround(transform.position, new(0, 0, 1), isClockwire ? -90 : 90);
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
