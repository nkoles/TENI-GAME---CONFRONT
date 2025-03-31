using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public int xDir = 0, yDir = 0;
    public float speed = 2.5f;
    public int damage = 5;
    public float delay = 1.0f, timer = 3.0f;
    public bool moving = true;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.y > 0)
        {
            xDir = 0;
            yDir = -1;
        }
        if(gameObject.transform.position.y < 0)
        {
            xDir = 0;
            yDir = 1;
        }
        if(gameObject.transform.position.x < 0)
        {
            xDir = 1;
            yDir = 0;
        }
        if(gameObject.transform.position.x > 0)
        {
            xDir = -1;
            yDir = 0;
        }
    }

    void FixedUpdate()
    {
        if(moving)
        {
            rb.velocity = new Vector2(xDir * speed, yDir * speed);
        }
    }

    public IEnumerator Close()
    {
        moving = false;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Walls")
        {
            StartCoroutine(Close());
        }
    }
}
