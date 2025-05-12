using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float speed = 1;
    public int xDir = 0, yDir = 0;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        started = false;

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

        StartCoroutine(Wait());
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(started)
        {
            rb.velocity = new Vector2(xDir * speed, yDir * speed);
        }
    }

    public IEnumerator Wait()
    {
        sr.enabled = true;
        
        yield return new WaitForSeconds(1.0f);

        started = true;
    }
}
