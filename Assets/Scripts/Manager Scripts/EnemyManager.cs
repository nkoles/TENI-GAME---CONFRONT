using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    public int hp, maxHP;

    public string[] dialogueText;
    public GameObject[] parkObstacles;
    public GameObject[] schoolObstacles;
    public GameObject[] homeObstacles;
    public GameObject[] clinicObstacles;
    public string[] AttackingWords;
    public Vector3[] lateralSpawnPoints;
    public string[] enemyWords;
    public int phase;
    public float timeMod;

    public GameObject arrow;

    //public DialogueManager dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Blocking(40f));
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Blocking(int amount)
    {
        Transform diamond = GameplayManager.Instance.player.diamond.transform;

        for(int i = 0; i < (amount); i++)
        {
            int randPhrase = Random.Range(0, AttackingWords.Length);
            int lastChar = 0;

            while(lastChar < AttackingWords[randPhrase].Length)
            {
                int randPoint = Random.Range(0, lateralSpawnPoints.Length);
                string phrase = "";
                
                while(AttackingWords[randPhrase][lastChar].ToString() != " ")
                {
                    phrase += AttackingWords[randPhrase][lastChar];
                    lastChar++;
                }

                GameObject temp = Instantiate(arrow, diamond.position /*+ new Vector3(lateralSpawnPoints[randPoint].x * diamond.localScale.x, lateralSpawnPoints[randPoint].y * diamond.localScale.y, 0)*/, Quaternion.Euler(0, 0, 45 /*- (90 * randPoint)*/));

                temp.transform.localScale = diamond.localScale;

                temp.GetComponent<ArrowScript>().word.text = phrase;
                temp.GetComponent<ArrowScript>().modifier = diamond.localScale;

                lastChar++;

                int randTime = Random.Range(1, 4);
                float time = timeMod*randTime;

                yield return new WaitForSeconds(time);
            }
        }

        /*while(time > 4)
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
        }*/

        yield return new WaitForSeconds(((5+phase)*timeMod));
    }

    public void RemoveWeapon(int weapon)
    {
        switch(phase)
        {
            case 1:
            {
                schoolObstacles[weapon] = null;
                int obstacleAmount = 0;
                for(int i = 0; i < schoolObstacles.Length; i++)
                {
                    if(schoolObstacles[i] != null)
                    {
                        obstacleAmount++;
                    }
                }

                if(obstacleAmount < 3)
                {
                        GameplayManager.Instance.isWin = true;
                        GameplayManager.Instance.Win();
                }

                break;
            }

            case 2:
            {
                homeObstacles[weapon] = null;
                int obstacleAmount = 0;
                for(int i = 0; i < homeObstacles.Length; i++)
                {
                    if(homeObstacles[i] != null)
                    {
                        obstacleAmount++;
                    }
                }

                if(obstacleAmount < 3)
                {
                    GameplayManager.Instance.isWin = true;
                    GameplayManager.Instance.Win();
                }

                break;
            }

            case 3:
            {
                clinicObstacles[weapon] = null;
                int obstacleAmount = 0;
                for(int i = 0; i < clinicObstacles.Length; i++)
                {
                    if(clinicObstacles[i] != null)
                    {
                        obstacleAmount++;
                    }
                }

                if(obstacleAmount < 3)
                {
                    GameplayManager.Instance.isWin = true;
                    GameplayManager.Instance.Win();
                }

                break;
            }

            default:
            {
                parkObstacles[weapon] = null;

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
                    //dialogue.principalStart = true;
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
                List<GameObject> objects = new List<GameObject>();

                foreach(GameObject thing in parkObstacles)
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
                    GameObject temp = Instantiate(objects[randObject], Random.insideUnitCircle.normalized * 5, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
                    Destroy(temp, time);
                    objects.Remove(objects[randObject]);
                }

                break;
            }
        }

        for(int i = 0; i < time; i++)
        {
            if(GameplayManager.Instance.player.hp <= 0)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
