using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeartScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float speed, tempSpeed, speedModifier, stunTime, frequency = 5.0f, variation = .25f;
    private Vector2 direction;
    public InputActionReference movement;
    public bool slip = false, stick = false, stun = false;
    public Color color;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tempSpeed = speed;
        speedModifier = 1;
        stunTime = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = movement.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if(stun)
        {
            /*transform.position +=*/ rb.velocity = new Vector3(Mathf.Cos(Time.time * frequency) * variation, Mathf.Sin(Time.time * frequency) * variation, 0);
        }
        if(slip && !stick)
        {
            tempSpeed = speed*5;
        }
        else if(stick && !slip)
        {
            tempSpeed = speed/5;
        }
        else
        {
            tempSpeed = speed;
        }

        if(!stun)
        {
            rb.velocity = new Vector2(direction.x * tempSpeed * speedModifier, direction.y * tempSpeed * speedModifier);
        }
    }

    public void OnDisable()
    {
        sr.color = color;
    }

    public IEnumerator Damage(int amount)
    {
        /*for(int i = 0; i < amount; i++)
        {*/
            for(int j = 0; j < 6; j++)
            {
                sr.color = new Color(255, 255, 255, 0);
                
                yield return new WaitForSeconds(.125f);

                sr.color = color;
                
                yield return new WaitForSeconds(.125f);
            }
        //}
    }

    public IEnumerator Stun(float time)
    {
        if(!stun)
        {
            rb.velocity = new Vector3(0, 0, 0);
            stun = true;
            //stunTime = time;

            /*while(stunTime > 0)
            {
                //float currentStunTime = stunTime;

                yield return new WaitForSeconds(stunTimetime);

                //stunTime -= currentStunTime;
            }*/

            yield return new WaitForSeconds(/*stunTime*/time);

            stun = false;
        }
    }
}
