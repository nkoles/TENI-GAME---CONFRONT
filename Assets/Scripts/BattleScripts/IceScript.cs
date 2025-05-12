using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 direction;
    public float speed = 1;
    public float rotation = 45;
    public float time = 0;
    public int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        direction = (Random.insideUnitCircle * 1.5f) - new Vector2(transform.position.x, transform.position.y);

        bool clockwise = Random.value >= 0.5f;
        if(clockwise)
        {
            speed *= -1;
        }

        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;

        rb.velocity = new Vector3(direction.normalized.x, direction.normalized.y, 0);
        rb.MoveRotation(rb.rotation + rotation*Time.fixedDeltaTime*speed);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);

            //HeartScript heart = collision.gameObject.GetComponent<HeartScript>();

            /*if(heart.stun == false)
            {
                heart.stun = true;
            }*/
            
            //heart.rb.velocity = rb.velocity;
            //heart.rb.AddForce((transform.position - collision.gameObject.transform.position), ForceMode2D.Impulse);
        }
    }
}
