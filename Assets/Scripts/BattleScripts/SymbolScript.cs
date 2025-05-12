using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Transform target;
    public Vector3 direction;
    public float speed = 1f, modifier;
    public int damage = 1;
    public string symbol;
    public float time = 0, timer = .5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        target = GameplayManager.Instance.player.heart.transform;
        //direction = (Random.insideUnitCircle * 1.5f) - new Vector2(transform.position.x, transform.position.y);

        if(symbol != "Female" && symbol != "Trans")
        {
            //Vector3 diagonal = Vector3.Cross(direction, transform.up);
            StartCoroutine(DishMovement(/*diagonal*/45));
        }
        /*else
        {
            transform.rotation = Quaternion.Euler(0,0, Mathf.Atan2(target.position.y, target.position.x)*Mathf.Rad2Deg);
            sr.enabled = true;
        }*/

        bool clockwise = Random.value >= 0.5f;
        if(clockwise)
        {
            modifier = -1;
        }
        else
        {
            modifier = 1;
        }

        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(symbol == "Female")
        {
            rb.velocity = new Vector3((target.position - transform.position).normalized.x * speed, (target.position - transform.position).normalized.y * speed);
        }
        else if(symbol == "Trans")
        {
            time++;

            rb.velocity = new Vector2(((target.position - transform.position).normalized.x*speed) + (Mathf.Cos(time*modifier/**Time.fixedDeltaTime*/*Mathf.Deg2Rad*5) * 1), ((target.position - transform.position).normalized.y*speed) + (Mathf.Sin(time*modifier/**Time.fixedDeltaTime*/*Mathf.Deg2Rad*5) * 1));
        }

        rb.MoveRotation(rb.rotation + (/*Time.fixedDeltaTime**/speed*modifier));
    }

    public IEnumerator DishMovement(/*Vector3 diagonal*/float angle)
    {
        Vector3 newDirection = Quaternion.Euler(0, 0, angle) * (target.position - transform.position);
        rb.velocity = new Vector2((newDirection.normalized.x /*+ diagonal.normalized.x*/) * speed * 2, (newDirection.normalized.y /*+ diagonal.normalized.y*/) * speed * 2);

        yield return new WaitForSeconds(timer);

        StartCoroutine(DishMovement(/*-diagonal*/-angle));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);
        }
    }
}
