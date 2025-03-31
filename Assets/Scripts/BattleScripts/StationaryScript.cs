using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StationaryScript : MonoBehaviour
{
    public int damage, timer;
    public float speed = .5f;
    public string type;
    public Tile tile;
    public Tilemap tilemap;
    private Tilemap secondaryTilemap;
    public Rigidbody2D rb;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        if(tilemap == null)
        {
            tilemap = GameObject.FindWithTag(type + " Tilemap").GetComponent<Tilemap>();
        }
        if(type != "Walls")
        {
            secondaryTilemap = GameObject.FindWithTag("Walls Tilemap").GetComponent<Tilemap>();
        }

        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = transform.position / tilemap.gameObject.transform.localScale.z;
        vector = Quaternion.Euler(0, 0, -45) * vector;

        if(tilemap.GetTile(Vector3Int.FloorToInt(vector)) != tile)
        {
            //Vector3Int.FloorToInt(vector);
            tilemap.SetTile(Vector3Int.FloorToInt(vector), tile);
        }
        if(secondaryTilemap != null)
        {
            if(secondaryTilemap.GetTile(Vector3Int.FloorToInt(vector)) != tile)
            {
                vector = transform.position / secondaryTilemap.gameObject.transform.localScale.z;
                vector = Quaternion.Euler(0, 0, -45) * vector;
                //Vector3Int.FloorToInt(vector);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(vector), null);
            }
        }

        if(Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) >= 3.0f)
        {
            direction = new Vector2(direction.x * -1, direction.y * -1);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    public IEnumerator ChangeDirection()
    {
        if(direction == null)
        {
            bool equalAxis = Random.value < 0.5f;

            if(equalAxis)
            {
                bool dir = Random.value < 0.5f;
                
                if(dir)
                {
                    direction = new Vector2(1, 1);
                }
                else
                {
                    direction = new Vector2(-1, -1);
                }
            }
            else
            {
                bool dir = Random.value <0.5f;
                
                if(dir)
                {
                    direction = new Vector2(1, -1);
                }
                else
                {
                    direction = new Vector2(-1, 1);
                }
            }
        }
        else
        {
            bool change = Random.value < 0.5f;
            Debug.Log(change);

            if(change)
            {
                bool turn = Random.value < 0.5f;

                if(turn)
                {
                    direction = new Vector2 (direction.x, direction.y * -1);
                }
                else
                {
                    direction = new Vector2 (direction.x * -1, direction.y);
                }
            }
        }

        yield return new WaitForSeconds(timer);

        StartCoroutine(ChangeDirection());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);
        }
    }
}
