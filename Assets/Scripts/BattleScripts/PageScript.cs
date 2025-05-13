using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PageScript : MonoBehaviour
{
    public Tile slip, stick, damage;
    private Vector3 playerPos;
    private HeartScript player;

    public bool isDamaged;
    public float damageInvulTime = 3f;
    public float tempTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameplayManager.Instance.player.heart.GetComponent<HeartScript>();
        playerPos = player.transform.position / transform.localScale.z;
        playerPos = Quaternion.Euler(0, 0, 45) * playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameplayManager.Instance.player.heart.transform.position / transform.localScale.z;
        playerPos = Quaternion.Euler(0, 0, 45) * playerPos;

        if (isDamaged)
        {
            if(tempTimer < damageInvulTime)
            {
                tempTimer += Time.deltaTime;
            } else
            {
                tempTimer = 0;
                isDamaged = false;
            }
        }

        if (gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(playerPos)) == slip)
        {
            player.slip = true;
            player.stick = false;
        }
        else if (gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(playerPos)) == stick)
        {
            player.stick = true;
            player.slip = false;
        }
        else if (gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(playerPos)) == damage)
        {
            player.slip = false;
            player.stick = false;

            if (!isDamaged)
            {
                GameplayManager.Instance.UpdatePlayerHealth(1);
                isDamaged = true;
            }
        }
        else
        {
            player.slip = false;
            player.stick = false;
        }
    }

    /*void OnTriggerStay2D(Collider2D collider)
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
            else if(gameObject.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(vector)) == damage)
            {
                GameplayManager.Instance.UpdatePlayerHealth(1);
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
    }*/
}
