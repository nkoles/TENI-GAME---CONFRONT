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
    public int xDir = 0, yDir = 0;
    public float speed = 2.5f;
    public string key;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(gameObject.transform.position.y > 0)
        {
            xDir = 0;
            yDir = -1;

            key = "Up";
        }
        if(gameObject.transform.position.y < 0)
        {
            xDir = 0;
            yDir = 1;

            key = "Down";
        }
        if(gameObject.transform.position.x < 0)
        {
            xDir = 1;
            yDir = 0;

            key = "Left";
            Vector3 vector = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = vector;
            //canvas.GetComponent<RectTransform>().anchoredPosition = 1 + text.text.Length/20;
            //Debug.Log(vector);
        }
        if(gameObject.transform.position.x > 0)
        {
            xDir = -1;
            yDir = 0;

            key = "Right";
            Vector3 vector = new Vector3(1 + ((float)word.text.Length/20), 1 + ((float)word.text.Length/20), 0);
            canvas.GetComponent<RectTransform>().localPosition = vector;
            //canvas.GetComponent<RectTransform>().anchoredPosition = 1 + text.text.Length/20;
            //Debug.Log(vector);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * speed, yDir * speed);
    }
}
