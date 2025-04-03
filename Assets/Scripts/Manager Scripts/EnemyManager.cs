using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public int hp, maxHP;

    public string[] dialogueText;
    public GameObject[] schoolObstacles;
    public string[] SchoolAttackingWords;
    public Vector3[] lateralSpawnPoints;
    public string[] enemyWords;
    public int phase;

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
            int randPhrase = Random.Range(0, SchoolAttackingWords.Length);
            int lastChar = 0;

            while(time > 4 && lastChar < SchoolAttackingWords[randPhrase].Length)
            {
                int randPoint = Random.Range(0, lateralSpawnPoints.Length);
                string phrase = "";
                
                while(SchoolAttackingWords[randPhrase][lastChar].ToString() != " ")
                {
                    phrase += SchoolAttackingWords[randPhrase][lastChar];
                    lastChar++;
                }

                GameObject temp = Instantiate(arrow, lateralSpawnPoints[randPoint], Quaternion.Euler(0, 0, 45 - (90 * randPoint)));

                temp.GetComponent<ArrowScript>().word.text = phrase;
                lastChar++;

                int randTime = Random.Range(1, 5);

                yield return new WaitForSeconds(randTime);

                time -= randTime;
            }
        }
    }

    public IEnumerator Dodging(float time, int difficulty)
    {
        switch(phase)
        {
            case 1:
            {
                List<GameObject> objects = new List<GameObject>();

                foreach(GameObject thing in schoolObstacles)
                {
                    objects.Add(thing);
                }

                for(int i = 0; i < difficulty; i++)
                {
                    int randObject = Random.Range(0, objects.Count);

                    if(objects[randObject] == schoolObstacles[0])
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[1])
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[2])
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[3])
                    {
                        GameObject temp = Instantiate(objects[randObject], GameplayManager.Instance.player.heart.transform.position, Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[4])
                    {
                        Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[5])
                    {
                        Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        objects.Remove(objects[randObject]);
                    }
                }

                break;
            }

            default:
            {
                break;
            }
        }

        yield return new WaitForSeconds(time);
    }
}
