using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerSpawner : MonoBehaviour
{
    public GameObject ruler;
    public float time;
    public List<Vector3> spawnPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        /*Vector3 vector = new Vector3(5,5,0);

        for(int i = 0; i < 4; i++)
        {
            if(vector.x == vector.y)
            {
                spawnPoints.Add(new Vector3(1 + vector.x, 1 - vector.y, 0));
                spawnPoints.Add(new Vector3(vector.x, vector.y, 0));
                spawnPoints.Add(new Vector3(1 - vector.x, 1 + vector.y, 0));
            }
            if(vector.x == -vector.y)
            {
                spawnPoints.Add(new Vector3(1 + vector.x, 1 + vector.y, 0));
                spawnPoints.Add(new Vector3(vector.x, vector.y, 0));
                spawnPoints.Add(new Vector3(1 - vector.x, 1 - vector.y, 0));
            }

            vector = Quaternion.Euler(0, 0, -90) * vector;
        }*/

        StartCoroutine(SpawnRuler());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnRuler()
    {
        float tempTime = time;
        while(tempTime > 4)
        {
            int randNo = Random.Range(-1, 2);
            if(randNo >= 0)
            {
                List<int> sides = new List<int>{0,1,2,3};

                for(int i = 0; i <= randNo; i++)
                {
                    int randPoint = Random.Range(0, 3);
                    int randSide = Random.Range(0, sides.Count);

                    Instantiate(ruler, spawnPoints[(sides[randSide] * 3) + randPoint], Quaternion.Euler(0, 0, -45 + (90 * Mathf.FloorToInt(sides[randSide]))));
                    sides.RemoveAt(randSide);
                }
            }

            int randTime = Random.Range(1, 5);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }
}
