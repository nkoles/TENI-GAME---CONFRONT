using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampScript : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D collider;
    public Color inactive, active;
    public float inactiveTime, activeTime;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        transform.position = (Random.insideUnitCircle * 1.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90, 90));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sr.color.a >= 1 && sr.color != active)
        {
            StartCoroutine(Stamp());
        }
        else if(sr.color != active)
        {
            float a = sr.color.a;
            a += Time.fixedDeltaTime/inactiveTime;

            if(a > 1)
            {
                a = 1;
            }

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
        }
    }

    public IEnumerator Stamp()
    {
        sr.color = active;
        collider.enabled = true;

        yield return new WaitForSeconds(activeTime);

        sr.color = inactive;
        collider.enabled = false;

        transform.position = (Random.insideUnitCircle * 1.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90, 90));
    }
}
