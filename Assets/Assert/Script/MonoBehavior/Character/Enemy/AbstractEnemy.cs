using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{

    [HideInInspector]
    public bool isHit = false;  // �Ƿ��ܵ�����

    abstract public void Hit(Transform source, int damage);

}
