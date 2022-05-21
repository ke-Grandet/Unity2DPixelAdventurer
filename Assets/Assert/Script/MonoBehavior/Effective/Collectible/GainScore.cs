using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainScore : MonoBehaviour
{
    [Header("½±Àø·ÖÊý")]
    public int score = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("Gain");
            GameManager.Instance.GainScore(score);
        }
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
