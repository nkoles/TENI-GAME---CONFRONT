using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float speed = 1;
    public float rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y > 0)
        {
            bool clockwise = Random.value >= 0.5f;
            if(clockwise)
            {
                sr.flipY = true;
                rotation = -45;
            }
            else
            {
                rotation = 45;
            }
        }
        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y < 0)
        {
            bool clockwise = Random.value >= 0.5f;
            if(clockwise)
            {
                sr.flipY = true;
                rotation = -45;
            }
            else
            {
                rotation = 45;
            }
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y > 0)
        {
            bool clockwise = Random.value >= 0.5f;
            if(clockwise)
            {
                sr.flipY = true;
                rotation = -45;
            }
            else
            {
                rotation = 45;
            }
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            bool clockwise = Random.value >= 0.5f;
            if(clockwise)
            {
                sr.flipY = true;
                rotation = -45;
            }
            else
            {
                rotation = 45;
            }
        }

        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + ((rotation * Time.fixedDeltaTime) * speed));
    }
}
