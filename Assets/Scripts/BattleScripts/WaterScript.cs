using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Vector3 direction;
    public float minSpeed, speed, maxSpeed, turnSpeed = 45;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        direction = (Random.insideUnitCircle * 1.5f) - new Vector2(transform.position.x, transform.position.y);

        transform.rotation = Quaternion.Euler(0,0, 90 + Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg);
        sr.enabled = true;

        speed = Random.Range(minSpeed, maxSpeed);

        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.rotation.z != -360 || transform.rotation.z != 0 || transform.rotation.z != 360)
        {
            //transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up);

            //transform.rotation = Quaternion.Euler(0,0, 90 + Mathf.Atan2(rb.velocity.y, rb.velocity.x)*Mathf.Rad2Deg); // Try This Method Sometime?

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), turnSpeed*Time.fixedDeltaTime);
        }

        //rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            if(collider.gameObject.GetComponent<HeartScript>().stun == false)
            {
                StartCoroutine(collider.gameObject.GetComponent<HeartScript>().Stun(1.0f));
            }
        }
    }
}
