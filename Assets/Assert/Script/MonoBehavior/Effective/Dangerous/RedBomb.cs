using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBomb : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GetComponent<Animator>().SetTrigger("Bomb");
            player.Hit();
        }
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 9))
        {
            GetComponent<Animator>().SetTrigger("Bomb");
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
