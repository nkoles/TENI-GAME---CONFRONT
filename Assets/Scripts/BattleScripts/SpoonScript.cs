using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    public float time = 0;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        bool clockwise = Random.value >= 0.5f;
        rb = gameObject.GetComponent<Rigidbody2D>();

        if(clockwise)
        {
            speed = -1;
        }
        else
        {
            speed = 1;
        }

        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y > 0)
        {
            offset = 0;
        }
        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y < 0)
        {
            offset = 90;
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y > 0)
        {
            offset = 180;
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            offset = 270;
        }

        Destroy(gameObject, 30.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;

        rb.velocity = new Vector2((Mathf.Cos(offset*Mathf.Deg2Rad + time*Time.fixedDeltaTime*Mathf.Deg2Rad)) * speed, (Mathf.Sin(offset*Mathf.Deg2Rad + time*Time.fixedDeltaTime*Mathf.Deg2Rad)) * speed);
        rb.MoveRotation(rb.rotation + (Time.fixedDeltaTime*speed));
    }
}
