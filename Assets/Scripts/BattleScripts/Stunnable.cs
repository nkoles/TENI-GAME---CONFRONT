using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    void OnTriggerStay2D(Collider2D collider)
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
