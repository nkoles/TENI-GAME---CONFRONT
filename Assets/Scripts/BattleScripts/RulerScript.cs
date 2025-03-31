using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerScript : MonoBehaviour
{
    public bool moving = false;
    public float delay = 1.0f, timer = 3.0f;
    public int damage = 5;
    public float speed = 5.0f;
    public int xDir = 0, yDir = 0;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y > 0)
        {
            xDir = -1;
            yDir = -1;
        }
        if(gameObject.transform.position.x > 0 && gameObject.transform.position.y < 0)
        {
            xDir = -1;
            yDir = 1;
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y > 0)
        {
            xDir = 1;
            yDir = -1;
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            xDir = 1;
            yDir = 1;
        }
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving)
        {
            rb.velocity = new Vector2(xDir * speed, yDir * speed);
        }
    }

    public IEnumerator Move()
    {
        yield return new WaitForSeconds(delay);
        moving = true;
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);
        }
    }
}
