using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PageScript : MonoBehaviour
{
    public Tile slip, stick;

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
            Vector3 vector = collider.gameObject.transform.position / transform.localScale.z;
            vector = Quaternion.Euler(0, 0, -45) * vector;

            if(gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(vector)) == slip)
            {
                collider.gameObject.GetComponent<HeartScript>().slip=true;
                collider.gameObject.GetComponent<HeartScript>().stick=false;
            }
            else if(gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(vector)) == stick)
            {
                collider.gameObject.GetComponent<HeartScript>().stick=true;
                collider.gameObject.GetComponent<HeartScript>().slip=false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            collider.gameObject.GetComponent<HeartScript>().slip=false;
            collider.gameObject.GetComponent<HeartScript>().stick=false;
        }
    }
}
