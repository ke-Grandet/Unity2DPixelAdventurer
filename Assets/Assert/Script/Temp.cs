using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    public Transform point;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("按键V");
            RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.right, 5, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                Debug.Log("碰到了" + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("是玩家");
                }
                else
                {
                    Debug.Log("是：" + hit.collider.name);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (point != null)
        {
            Gizmos.DrawRay(point.position, Vector2.right * 5);
        }
    }

}
