using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    // ��ʾʤ������
        //    GameController.Instance.ShowGameVictoryPanel();
        //    _boxCollider2D.enabled = false;
        //    // ������ҵĸ���
        //    collision.GetComponent<Rigidbody2D>().simulated = false;
        //    collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    // ������ҵĶ���
        //    collision.GetComponent<Animator>().enabled = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ʾʤ������
            GameController.Instance.ShowGameVictoryPanel();
            _rigidbody2D.simulated = false;
            _boxCollider2D.enabled = false;
            // ������ҵĸ���
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            // ������ҵĶ���
            collision.gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
