using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    public int xDir = 0, yDir = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.y > 0)
        {
            bool positive = Random.value >= 0.5f;
            if(positive)
            {
                xDir = 1;
                rb.rotation -= 45;
            }
            else
            {
                xDir = -1;
                rb.rotation += 45;
            }
            
            yDir = -1;
        }
        if(gameObject.transform.position.y < 0)
        {
            bool positive = Random.value >= 0.5f;
            if(positive)
            {
                xDir = 1;
                rb.rotation += 45;
            }
            else
            {
                xDir = -1;
                rb.rotation -= 45;
            }

            yDir = 1;
        }
        if(gameObject.transform.position.x < 0)
        {
            bool positive = Random.value >= 0.5f;
            if(positive)
            {
                yDir = 1;
                rb.rotation -= 45;
            }
            else
            {
                yDir = -1;
                rb.rotation += 45;
            }

            xDir = 1;
        }
        if(gameObject.transform.position.x > 0)
        {
            bool positive = Random.value >= 0.5f;
            if(positive)
            {
                yDir = 1;
                rb.rotation += 45;
            }
            else
            {
                yDir = -1;
                rb.rotation -= 45;
            }

            xDir = -1;
        }

        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * speed, yDir * speed);
    }
}
