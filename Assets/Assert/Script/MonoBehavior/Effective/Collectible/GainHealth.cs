using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHealth : MonoBehaviour
{
    [Header("������Դ")]
    public Transform source;
    [Header("��������ֵ")]
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
