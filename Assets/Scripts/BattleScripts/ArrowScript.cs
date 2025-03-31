using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int xDir = 0, yDir = 0;
    public float speed = 2.5f;
    public string key;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.y > 0)
        {
            xDir = 0;
            yDir = -1;

            key = "Up";
        }
        if(gameObject.transform.position.y < 0)
        {
            xDir = 0;
            yDir = 1;

            key = "Down";
        }
        if(gameObject.transform.position.x < 0)
        {
            xDir = 1;
            yDir = 0;

            key = "Left";
        }
        if(gameObject.transform.position.x > 0)
        {
            xDir = -1;
            yDir = 0;

            key = "Right";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * speed, yDir * speed);
    }
}
