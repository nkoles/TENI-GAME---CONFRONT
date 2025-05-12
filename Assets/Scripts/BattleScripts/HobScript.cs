using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobScript : MonoBehaviour
{
    public GameObject fire;
    public SpriteRenderer sr;
    public Color inactive, active;
    public float time, heatingTime;
    public bool hovering = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hovering)
        {
            time += Time.fixedDeltaTime;
            sr.color = Color.Lerp(inactive, active, time/heatingTime);
        }
        else if(time > 0)
        {
            time -= Time.fixedDeltaTime;
            sr.color = Color.Lerp(inactive, active, time/heatingTime);
        }

        if(time >= heatingTime)
        {
            fire.SetActive(true);
        }
        else if(fire.activeSelf == true)
        {
            fire.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            hovering = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            hovering = false;
        }
    }
}
