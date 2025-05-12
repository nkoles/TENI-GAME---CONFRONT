using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sr;
    public GameObject pusher, needle;
    private HeartScript heart;
    public Color inactive, active;
    public Vector3[] spawnPoints;
    public int time = 30, speed = 1;
    public bool inject = false, extract = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        heart = GameplayManager.Instance.player.heart.GetComponent<HeartScript>();

        StartCoroutine(Inject());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inject && !extract)
        {
            heart.speedModifier -= Time.fixedDeltaTime;
            heart.sr.color = Color.Lerp(active, inactive, (heart.speedModifier+1)/2);
            Debug.Log(heart.speedModifier);

            if(heart.speedModifier <= -1)
            {
                heart.speedModifier = -1;
                inject = false;
            }
        }

        if(extract && !inject)
        {
            heart.speedModifier += Time.fixedDeltaTime;
            heart.sr.color = Color.Lerp(active, inactive, (heart.speedModifier+1)/2);
            Debug.Log(heart.speedModifier);

            if(heart.speedModifier >= 1)
            {
                heart.speedModifier = 1;
                extract = false;
            }
        }
    }

    void OnDisable()
    {
        heart.speedModifier = 1;
        heart.sr.color = inactive;
    }

    public IEnumerator Inject()
    {
        if(time > 5)
        {
            int randPoint = Random.Range(0, spawnPoints.Length);

            transform.position = spawnPoints[randPoint];
            transform.rotation = Quaternion.Euler(0, 0, -90*randPoint);

            sr.enabled = true;
            pusher.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            needle.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(1.0f); // Spawn

            rb.velocity = new Vector2(-transform.position.normalized.x, -transform.position.normalized.y)*speed;

            yield return new WaitForSeconds(1.0f); // Enter

            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("Inject");
            inject = true;

            yield return new WaitForSeconds(1.0f); // Inject

            rb.velocity = new Vector2(transform.position.normalized.x, transform.position.normalized.y)*speed;

            yield return new WaitForSeconds(1.0f); // Exit

            rb.velocity = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(1.0f); // Despawn

            sr.enabled = false;
            pusher.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            needle.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            int randTime = Random.Range(5, 11);

            yield return new WaitForSeconds(randTime);

            time -= randTime;

            StartCoroutine(Extract());
        }
    }

    public IEnumerator Extract()
    {
        if(time > 5)
        {
            int randPoint = Random.Range(0, spawnPoints.Length);

            transform.position = spawnPoints[randPoint];
            transform.rotation = Quaternion.Euler(0, 0, -90*randPoint);

            sr.enabled = true;
            pusher.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            needle.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(1.0f); // Spawn

            rb.velocity = new Vector2(-transform.position.normalized.x, -transform.position.normalized.y)*speed;

            yield return new WaitForSeconds(1.0f); // Enter

            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("Extract");
            extract = true;

            yield return new WaitForSeconds(1.0f); // Extract

            rb.velocity = new Vector2(transform.position.normalized.x, transform.position.normalized.y)*speed;

            yield return new WaitForSeconds(1.0f); // Exit

            rb.velocity = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(1.0f); // Despawn

            sr.enabled = false;
            pusher.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            needle.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            int randTime = Random.Range(5, 11);

            yield return new WaitForSeconds(randTime);

            time -= randTime;

            StartCoroutine(Inject());
        }
    }
}
