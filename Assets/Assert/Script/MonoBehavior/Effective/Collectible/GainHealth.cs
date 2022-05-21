using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHealth : MonoBehaviour
{
    [Header("治疗来源")]
    public Transform source;
    [Header("奖励生命值")]
    public int healValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GetComponent<Collider2D>().enabled = false;
            player.Heal(source, healValue);
            GetComponent<Animator>().SetTrigger("Gain");
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
