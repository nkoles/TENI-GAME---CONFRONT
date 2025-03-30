using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlippyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            collider.gameObject.GetComponent<HeartScript>().slip=true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            collider.gameObject.GetComponent<HeartScript>().slip=false;
        }
    }
}
