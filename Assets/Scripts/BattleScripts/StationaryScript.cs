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
    private Tile currentTile;
    private Vector3 previousPos;
    public Tilemap tilemap;
    public Tilemap secondaryTilemap;
    public Rigidbody2D rb;
    public Vector2 direction;
    public Vector3 target;

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

        float randX = Random.Range(-3.0f, 3.0f);
        float randY = Random.Range((-3.0f + Mathf.Abs(randX)) * -1, 3.0f - Mathf.Abs(randX));
        target = new Vector3(randX, randY, 0);
        direction = Vector3.Normalize(target - transform.position);

        //StartCoroutine(ChangeDirection());
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
            vector = transform.position / secondaryTilemap.gameObject.transform.localScale.z;
            vector = Quaternion.Euler(0, 0, -45) * vector;

            if(Vector3.Distance(transform.position, previousPos) >= .25f /*secondaryTilemap.GetTile(Vector3Int.FloorToInt(vector)) != currentTile*/)
            {
                previousPos = transform.position;
                //currentTile = secondaryTilemap.GetTile<Tile>(Vector3Int.FloorToInt(vector));
                //Vector3Int.FloorToInt(vector);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(vector), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x, vector.y+1, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x+1, vector.y+1, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x+1, vector.y, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x+1, vector.y-1, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x, vector.y-1, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x-1, vector.y-1, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x-1, vector.y, vector.z)), null);
                secondaryTilemap.SetTile(Vector3Int.FloorToInt(new Vector3(vector.x-1, vector.y+1, vector.z)), null);
            }
        }

        /*if(Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) >= 3.0f)
        {
            direction = new Vector2(direction.x * -1, direction.y * -1);
        }*/
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if(Vector3.Distance(target, transform.position) < .25f/*transform.position.x <= target.x + .25f && transform.position.x >= target.x -.25f && transform.position.y <= target.y + .25f && transform.position.y >= target.y -.25f*/)
        {
            float randX = Random.Range(-3.0f, 3.0f);
            float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
            target = new Vector3(randX, randY, 0);
            direction = Vector3.Normalize(target - transform.position);
        }
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
            //Debug.Log(change);

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

    void OnDisable()
    {
        if(tilemap != null)
        {
            tilemap.ClearAllTiles();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<HeartScript>())
        {
            GameplayManager.Instance.UpdatePlayerHealth(damage);
        }
    }
}
