using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ArrowScript : MonoBehaviour
{
    public TMP_Text word;
    public GameObject canvas;
    public Rigidbody2D rb;
    public float xDir = 0, yDir = 1;
    public float speed = 2.5f;
    public string key;
    public int phase;
    public bool opposite, adjacent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        canvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        phase = GameplayManager.Instance.enemy.phase;

        int target = Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0, 0, 45 - (90 * target));

        Vector3 vector = new Vector3(0, -1, 0);

        if(target == 0)
        {
            vector = new Vector3(0, -1, 0);
            key = "Up";
        }
        else if(target == 1)
        {
            vector = new Vector3(-1, 0, 0);
            key = "Right";
        }
        else if(target == 2)
        {
            vector = new Vector3(0, 1, 0);
            key = "Down";
        }
        else if(target == 3)
        {
            vector = new Vector3(1, 0, 0);
            key = "Left";
        }

        if(phase == 1)
        {
            bool opposite = Random.value >= 0.5f;
            if(opposite)
            {
                vector = Quaternion.Euler(0, 0, -180) * vector;
            }
        }
        if(phase == 2)
        {
            bool adjacent = Random.value >= 0.5f;
            if(adjacent)
            {
                bool clockwise = Random.value >= 0.5f;
                if(clockwise)
                {
                    vector = Quaternion.Euler(0, 0, -90) * vector;
                }
                else
                {
                    vector = Quaternion.Euler(0, 0, -270) * vector;
                }
            }
        }
        if(phase == 3)
        {
            bool adjacent = Random.value >= 0.5f;
            bool opposite = Random.value >= 0.5f;
            if(!adjacent)
            {
                if(opposite)
                {
                    vector = Quaternion.Euler(0, 0, -180) * vector;
                }
            }
            else
            {
                if(!opposite)
                {
                    vector = Quaternion.Euler(0, 0, -90) * vector;
                }
                else
                {
                    vector = Quaternion.Euler(0, 0, -270) * vector;
                }
            }
        }

        xDir = vector.x;
        yDir = vector.y;

        if(xDir > 0.5f)
        {
            Vector3 wordPos = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = wordPos;
        }
        else if(xDir < 0.5f)
        {
            Vector3 wordPos = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = wordPos;
        }

        /*if(gameObject.transform.position.y > 0 && gameObject.transform.position.x == 0)
        {
            xDir = 0;
            yDir = -1;

            key = "Up";
        }
        if(gameObject.transform.position.y < 0 && gameObject.transform.position.x == 0)
        {
            xDir = 0;
            yDir = 1;

            key = "Down";
        }
        if(gameObject.transform.position.x < 0 && gameObject.transform.position.x == 0)
        {
            xDir = 1;
            yDir = 0;

            key = "Left";
            Vector3 vector = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = vector;
            //canvas.GetComponent<RectTransform>().anchoredPosition = 1 + text.text.Length/20;
            //Debug.Log(vector);
        }
        if(gameObject.transform.position.x > 0 && gameObject.transform.position.x == 0)
        {
            xDir = -1;
            yDir = 0;

            key = "Right";
            Vector3 vector = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = vector;
            //canvas.GetComponent<RectTransform>().anchoredPosition = 1 + text.text.Length/20;
            //Debug.Log(vector);
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * speed, yDir * speed);
    }
}
