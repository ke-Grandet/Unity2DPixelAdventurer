using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainElement : MonoBehaviour
{

    [Header("‘™Àÿ÷÷¿‡")]
    public ElementEnum element;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ElementController.Instance.GainElement(element);
            Destroy(gameObject);
        }
    }

}
