using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TapeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Tilemap tilemap;
    public Tile middle, endTR, endBR, endBL, endTL;
    private Vector3 vector, tilemapVector;
    private Vector3 previousPos;
    public float speed = 1f, rotation;
    public int damage;
    public float waitTime, moveTime, delayTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        if(tilemap == null)
        {
            tilemap = GameObject.FindWithTag("Walls Tilemap").GetComponent<Tilemap>();
        }

        vector = transform.position / tilemap.gameObject.transform.localScale.z;

        rotation = tilemap.gameObject.transform.rotation.z;

        StartCoroutine(ChangePosition());
    }

    // Update is called once per frame
    void Update()
    {
        vector = transform.position / tilemap.gameObject.transform.localScale.z;
        vector = Quaternion.Euler(0, 0, 45) * vector;

        if((previousPos - transform.position).sqrMagnitude >= .5625f /*tilemap.GetTile(Vector3Int.FloorToInt(vector)) != tile*/)
        {
            //Vector3Int.FloorToInt(vector);
            previousPos = transform.position;
            if(tilemap.GetTile(Vector3Int.FloorToInt(vector)) == null)
            {
                tilemap.SetTile(Vector3Int.FloorToInt(vector), middle);
            }
        }
    }

    public IEnumerator ChangePosition()
    {
        float randX, randY;
        float xMod, yMod;
        
        bool x = Random.value >= 0.5f;
        if(x)
        {
            xMod = 1;
        }
        else
        {
            xMod = -1;
        }
        randX = xMod/2;

        bool y = Random.value >= 0.5f;
        if(y)
        {
            yMod = 1;
        }
        else
        {
            yMod = -1;
        }
        randY = yMod/2;

        while(Mathf.Abs(randX) <= 3 && Mathf.Abs(randY) <= 3)
        {
            bool rand = Random.value >= 0.5f;
            if(rand)
            {
                randX += xMod;
            }
            else
            {
                randY += yMod;
            }
        }

        Debug.Log(randX + ", " + randY);

        Vector3 target = new Vector3(randX, randY, 0) * tilemap.gameObject.transform.localScale.z;
        target = Quaternion.Euler(0, 0, -45) * target;

        transform.position = target;

        Vector3 direction = new Vector3(0, 0, 0);

        if(Mathf.Abs(randX) > 3 && Mathf.Abs(randY) <= 3)
        {
            direction = new Vector3(-randX, randY, 0) * tilemap.gameObject.transform.localScale.z;
        }
        else if(Mathf.Abs(randX) <= 3 && Mathf.Abs(randY) > 3)
        {
            direction = new Vector3(randX, -randY, 0) * tilemap.gameObject.transform.localScale.z;
        }

        direction = Quaternion.Euler(0, 0, -45) * direction;
        transform.rotation = Quaternion.Euler(0,0, 90 - Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x)*Mathf.Rad2Deg);

        sr.enabled = true;

        target = transform.position / tilemap.gameObject.transform.localScale.z;
        target = Quaternion.Euler(0, 0, 45) * target;

        Tile tempTile = middle;;

        if(Mathf.Abs(randX) > 3 && Mathf.Abs(randY) <= 3)
        {
            if(randX > 0)
            {
                tilemap.SetTile(Vector3Int.FloorToInt(target), endBR);
                tempTile = endTL;
                direction = new Vector3(-1, 1, 0);
            }
            else
            {
                tilemap.SetTile(Vector3Int.FloorToInt(target), endTL);
                tempTile = endBR;
                direction = new Vector3(1, -1, 0);
            }
        }
        else if(Mathf.Abs(randX) <= 3 && Mathf.Abs(randY) > 3)
        {
            if(randY > 0)
            {
                tilemap.SetTile(Vector3Int.FloorToInt(target), endTR);
                tempTile = endBL;
                direction = new Vector3(-1, -1, 0);
            }
            else
            {
                tilemap.SetTile(Vector3Int.FloorToInt(target), endBL);
                tempTile = endTR;
                direction = new Vector3(1, 1, 0);
            }
        }

        yield return new WaitForSeconds(waitTime);

        rb.velocity = new Vector2((direction.x) * speed, (direction.y) * speed);

        yield return new WaitForSeconds(moveTime);

        rb.velocity = new Vector3(0, 0, 0);

        target = transform.position / tilemap.gameObject.transform.localScale.z;
        target = Quaternion.Euler(0, 0, 45) * target;
        tilemap.SetTile(Vector3Int.FloorToInt(target), tempTile);

        sr.enabled = false;

        yield return new WaitForSeconds(delayTime);

        StartCoroutine(ChangePosition());
    }

    void OnDisable()
    {
        if(tilemap != null)
        {
            tilemap.ClearAllTiles();
        }
    }
}
