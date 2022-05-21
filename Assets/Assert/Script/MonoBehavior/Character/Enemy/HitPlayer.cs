using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{

    [Header("������")]
    public int damage;
    [Header("�˺���Դ")]
    public Transform source;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Hit(source, damage);
        }
    }

}
