using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{

    [Header("¹¥»÷Á¦")]
    public int damage;
    [Header("ÉËº¦À´Ô´")]
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
