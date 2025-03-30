using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public int hp, maxHP;

    public string[] dialogueText;
    public GameObject[] obstacles;
    public Transform[] spawnPoints;
    public string[] enemyWords;
    public string[] playerWords;

    public GameObject arrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Blocking(40f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Blocking(float time)
    {
        while(time > 4)
        {
            int randPoint = Random.Range(0, 4);

            Instantiate(arrow, spawnPoints[randPoint].position, Quaternion.Euler(0, 0, 45 + (90 * randPoint)));

            int randTime = Random.Range(0, 5);

            yield return new WaitForSeconds(randTime);

            time -= randTime;
        }
    }
}
