using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    public float speed = 2;
    public float moveTime = 3;

    private bool directionRight = true;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (directionRight)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }

        timer += Time.deltaTime;

        if (timer > moveTime)
        {
            directionRight = !directionRight;
            timer = 0;
        }
    }
}
