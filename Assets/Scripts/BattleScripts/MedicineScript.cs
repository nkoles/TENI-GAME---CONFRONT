using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1, acceleration = 1.1f, modifier;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(transform.up.x, transform.up.y) * speed;

        bool clockwise = Random.value >= 0.5f;
        if(clockwise)
        {
            modifier = -1;
        }
        else
        {
            modifier = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + (/*Time.fixedDeltaTime**/speed*modifier));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float randRot = Random.Range(-44, 44);
        Vector3 direction = new Vector3(0, 0, 0);
        if(transform.position.x > 0 && transform.position.y > 0)
        {
            direction = new Vector3(-1, -1, 0);
        }
        else if(transform.position.x > 0 && transform.position.y < 0)
        {
            direction = new Vector3(-1, 1, 0);
        }
        else if(transform.position.x < 0 && transform.position.y < 0)
        {
            direction = new Vector3(1, 1, 0);
        }
        else if(transform.position.x < 0 && transform.position.y > 0)
        {
            direction = new Vector3(1, -1, 0);
        }

        direction = Quaternion.Euler(0, 0, randRot) * direction;

        rb.velocity = new Vector2(direction.x, direction.y) * acceleration;

        acceleration += .1f;
    }
}
