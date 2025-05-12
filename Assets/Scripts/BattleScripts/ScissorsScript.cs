using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 direction;
    public Vector2 target;
    public float speed = 1, maxSpeed = 3, turnSpeed = 45;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        target = (Random.insideUnitCircle * 3f);

        direction = target - new Vector2(transform.position.x, transform.position.y);

        transform.rotation = Quaternion.Euler(0,0, 180 + Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((target - new Vector2(transform.position.x, transform.position.y)).magnitude < .5f)
        {
            target = (Random.insideUnitCircle * 3f);
        }

        direction = target - new Vector2(transform.position.x, transform.position.y);

        rb.AddForce(direction.normalized * speed*Time.fixedDeltaTime, ForceMode2D.Impulse);

        if(rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(rb.velocity.y, rb.velocity.x)*Mathf.Rad2Deg);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0, 180 + Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg), turnSpeed*Time.fixedDeltaTime);
    }
}
