using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeHit : MonoBehaviour
{
    [Header("Ŀ�����")]
    public AbstractEnemy enemy;

    public void Hit(Transform source, int damage)
    {
        enemy.Hit(source, damage);
    }

}
