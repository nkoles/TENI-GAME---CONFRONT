using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public int hp, maxHP;

    public string[] dialogueText;
    public GameObject[] schoolObstacles;
    public GameObject[] homeObstacles;
    public GameObject[] clinicObstacles;
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
        Transform diamond = GameplayManager.Instance.player.diamond.transform;

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

                GameObject temp = Instantiate(arrow, diamond.position + new Vector3(lateralSpawnPoints[randPoint].x * diamond.localScale.x, lateralSpawnPoints[randPoint].y * diamond.localScale.y, 0), Quaternion.Euler(0, 0, 45 - (90 * randPoint)));

                temp.transform.localScale = diamond.localScale;

                temp.GetComponent<ArrowScript>().word.text = phrase;
                lastChar++;

                int randTime = Random.Range(1, 5);

                yield return new WaitForSeconds(randTime);

                time -= randTime;
            }
        }
    }

    public void RemoveWeapon(int weapon)
    {
        switch(phase)
        {
            case 1:
            {
                schoolObstacles[weapon] = null;

                break;
            }

            case 2:
            {
                homeObstacles[weapon] = null;

                break;
            }

            case 3:
            {
                clinicObstacles[weapon] = null;

                break;
            }

            default:
            {

                break;
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
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < difficulty; i++)
                {
                    if(objects.Count <= 0)
                    {
                        break;
                    }

                    int randObject = Random.Range(0, objects.Count);

                    if(objects[randObject] == schoolObstacles[0]) // Pencil
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[1]) // Eraser
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[2]) // Gluestick
                    {
                        float randX = Random.Range(-3.0f, 3.0f);
                        float randY = Random.Range((-3.0f + Mathf.Abs(randX)), 3.0f - Mathf.Abs(randX));
                        GameObject temp = Instantiate(objects[randObject], new Vector3(randX, randY, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[3]) // Stapler
                    {
                        GameObject temp = Instantiate(objects[randObject], GameplayManager.Instance.player.heart.transform.position, Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[4]) // BookSpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == schoolObstacles[5]) // RulerSpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                }

                break;
            }

            case 2:
            {
                List<GameObject> objects = new List<GameObject>();

                foreach(GameObject thing in homeObstacles)
                {
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < difficulty; i++)
                {
                    if(objects.Count == 0)
                    {
                        break;
                    }

                    int randObject = Random.Range(0, objects.Count);

                    if(objects[randObject] == homeObstacles[0]) // DishSpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == homeObstacles[1]) // CutlerySpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == homeObstacles[2]) // BubbleSpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == homeObstacles[3]) // IceSpawner
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0, 5, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == homeObstacles[4]) // WaterSpawner
                    {
                        float position;
                        float rotation;
                        bool upsideDown = Random.value >= 0.5f;
                        if(upsideDown)
                        {
                            position = 3;
                            rotation = 180;
                        }
                        else
                        {
                            position = -3;
                            rotation = 0;
                        }

                        GameObject temp = Instantiate(objects[randObject], new Vector3(position, 0, 0), Quaternion.Euler(0, 0, rotation));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == homeObstacles[5]) // Hob
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                }

                break;
            }

            case 3:
            {
                List<GameObject> objects = new List<GameObject>();

                foreach(GameObject thing in clinicObstacles)
                {
                    if(thing != null)
                    {
                        objects.Add(thing);
                    }
                }

                for(int i = 0; i < difficulty; i++)
                {
                    if(objects.Count == 0)
                    {
                        break;
                    }
                    
                    int randObject = Random.Range(0, objects.Count);

                    if(objects[randObject] == clinicObstacles[0]) // Denied Stamp
                    {
                        GameObject temp = Instantiate(objects[randObject], Random.insideUnitCircle * 1.5f, Quaternion.Euler(0, 0, Random.Range(-90, 90)));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == clinicObstacles[1]) // Scissors
                    {
                        GameObject temp = Instantiate(objects[randObject], Random.insideUnitCircle.normalized * 5.0f, Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == clinicObstacles[2]) // Syringe
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == clinicObstacles[3]) // Tablet
                    {
                        GameObject temp = Instantiate(objects[randObject], Random.insideUnitCircle.normalized * 1.5f, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == clinicObstacles[4]) // Tape
                    {
                        GameObject temp = Instantiate(objects[randObject], Random.insideUnitCircle.normalized * 3f, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
                        Destroy(temp, time);
                        objects.Remove(objects[randObject]);
                    }
                    else if(objects[randObject] == clinicObstacles[5]) // Gender Symbols
                    {
                        GameObject temp = Instantiate(objects[randObject], new Vector3(0,0,0), Quaternion.Euler(0,0,0));
                        Destroy(temp, time);
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
