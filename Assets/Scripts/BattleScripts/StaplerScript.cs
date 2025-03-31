using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerScript : MonoBehaviour
{
    public bool moving;
    public int damage;
    public float speed = 3.0f, followTime = 3.0f, stapleTime = 2.0f, rechargeTime = 1.0f;
    public Rigidbody2D rb;
    public BoxCollider2D hitbox;
    public GameObject heart, staple;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        heart = GameplayManager.Instance.player.heart;
        StartCoroutine(Staple());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = heart.transform.position - new Vector3(0, 0.5f, 0);
    }

    void FixedUpdate()
    {
        if(moving)
        {
            Vector3 direction = heart.transform.position - staple.transform.position;
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    public IEnumerator Staple()
    {
        moving = true;
        yield return new WaitForSeconds(followTime);
        moving = false;
        yield return new WaitForSeconds(stapleTime);
        staple.SetActive(true);
        hitbox.enabled = true;
        yield return new WaitForSeconds(rechargeTime);
        staple.SetActive(false);
        hitbox.enabled = false;
        StartCoroutine(Staple());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);
        }
    }
}
