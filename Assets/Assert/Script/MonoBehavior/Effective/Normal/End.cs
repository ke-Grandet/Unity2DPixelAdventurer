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
        //    // 显示胜利画面
        //    GameController.Instance.ShowGameVictoryPanel();
        //    _boxCollider2D.enabled = false;
        //    // 禁用玩家的刚体
        //    collision.GetComponent<Rigidbody2D>().simulated = false;
        //    collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    // 禁用玩家的动画
        //    collision.GetComponent<Animator>().enabled = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 显示胜利画面
            GameController.Instance.ShowGameVictoryPanel();
            _rigidbody2D.simulated = false;
            _boxCollider2D.enabled = false;
            // 禁用玩家的刚体
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            // 禁用玩家的动画
            collision.gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
