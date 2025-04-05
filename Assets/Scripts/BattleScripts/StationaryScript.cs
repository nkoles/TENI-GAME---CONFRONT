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
    //private Tile currentTile;
    private Vector3 previousPos;
    public Tilemap tilemap;
    //public Tilemap secondaryTilemap;
    public Rigidbody2D rb;
    public Vector2 direction;
    public Vector3 target;
    private Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        if(tilemap == null)
        {
            tilemap = GameObject.FindWithTag(type + " Tilemap").GetComponent<Tilemap>();
        }
        /*if(type != "Walls")
        {
            secondaryTilemap = GameObject.FindWithTag("Walls Tilemap").GetComponent<Tilemap>();
        }*/

        rb = gameObject.GetComponent<Rigidbody2D>();

        float randX = Random.Range(-3.0f, 3.0f);
        float randY = Random.Range((-3.0f + Mathf.Abs(randX)) * -1, 3.0f - Mathf.Abs(randX));
        target = new Vector3(randX, randY, 0);
        direction = Vector3.Normalize(target - transform.position);

        vector = transform.position / tilemap.gameObject.transform.localScale.z;

        previousPos = transform.position;

        //StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        vector = transform.position / tilemap.gameObject.transform.localScale.z;
        vector = Quaternion.Euler(0, 0, -45) * vector;

        if((previousPos - transform.position).sqrMagnitude >= .5625f /*tilemap.GetTile(Vector3Int.FloorToInt(vector)) != tile*/)
        {
            //Vector3Int.FloorToInt(vector);
            previousPos = transform.position;
            tilemap.SetTile(Vector3Int.FloorToInt(vector), tile);
        }
        /*if(secondaryTilemap != null)
        {
            vector = transform.position / secondaryTilemap.gameObject.transform.localScale.z;
            vector = Quaternion.Euler(0, 0, -45) * vector;*/

            //if((previousPos - transform.position).sqrMagnitude >= .5625f /*secondaryTilemap.GetTile(Vector3Int.FloorToInt(vector)) != currentTile*/)
            /*{
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
        }*/

        /*if(Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) >= 3.0f)
        {
            direction = new Vector2(direction.x * -1, direction.y * -1);
        }*/
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if((target - transform.position).sqrMagnitude <= .5625f /*Vector3.Distance(target, transform.position) < .25f*//*transform.position.x <= target.x + .25f && transform.position.x >= target.x -.25f && transform.position.y <= target.y + .25f && transform.position.y >= target.y -.25f*/)
        {
            float randX = Random.Range(-3.0f, 3.0f);
            float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
            target = new Vector3(randX, randY, 0);
            direction = Vector3.Normalize(target - transform.position);
        }
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
