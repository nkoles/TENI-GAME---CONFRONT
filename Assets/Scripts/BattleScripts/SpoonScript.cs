using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D[] hitboxes;

    public float speed = 1;
    public float time = 0;
    public float offset = 0;

    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        started = false;

        bool clockwise = Random.value >= 0.5f;

        if(clockwise)
        {
            speed = -1;
        }
        else
        {
            speed = 1;
            sr.flipY = true;
            foreach(BoxCollider2D hitbox in hitboxes)
            {
                hitbox.offset *= new Vector2(1, -1);
            }
        }

        if(gameObject.transform.position.x > 1)
        {
            offset = 90;
        }
        else if(gameObject.transform.position.y > 1)
        {
            offset = 180;
        }
        else if(gameObject.transform.position.x < -1)
        {
            offset = 270;
        }
        else if(gameObject.transform.position.y < -1)
        {
            offset = 360;
        }

        StartCoroutine(Wait());
        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(started)
        {
            time++;

            rb.velocity = new Vector2((Mathf.Cos((offset+(time*speed)/**Time.fixedDeltaTime*/)*Mathf.Deg2Rad)) * speed*3, (Mathf.Sin((offset+(time*speed)/**Time.fixedDeltaTime*/)*Mathf.Deg2Rad)) * speed*3);
            rb.MoveRotation(rb.rotation + (/*Time.fixedDeltaTime**/speed));

            //float impulse (1 * Mathf.Deg2Rad) * rb.inertia;
        }
    }

    public IEnumerator Wait()
    {
        rb.velocity = new Vector2(-transform.position.normalized.x*Mathf.Abs(speed), -transform.position.normalized.y*Mathf.Abs(speed));
        
        yield return new WaitForSeconds(1.25f);

        started = true;
    }
}
