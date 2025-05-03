using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
    public GameObject fork, knife, spoon;
    public List<Vector3> forkSpawnPoints, knifeSpawnPoints, spoonSpawnPoints;
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFork());
        StartCoroutine(SpawnKnife());
        StartCoroutine(SpawnSpoon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnFork()
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
                    int randPoint = Random.Range(0, sides.Count);

                    Instantiate(fork, forkSpawnPoints[sides[randPoint]], Quaternion.Euler(0, 0, 90 + (-90 * sides[randPoint])));
                    sides.RemoveAt(randPoint);
                }
            }

            int randTime = Random.Range(3, 6);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }

    public IEnumerator SpawnKnife()
    {
        float tempTime = time;
        while(tempTime > 8)
        {
            int randNo = Random.Range(-1, 2);
            if(randNo >= 0)
            {
                List<int> sides = new List<int>{0,1,2,3};

                for(int i = 0; i <= randNo; i++)
                {
                    int randPoint = Random.Range(0, sides.Count);

                    Instantiate(knife, knifeSpawnPoints[sides[randPoint]], Quaternion.Euler(0, 0, -135 + (-90 * sides[randPoint])));
                    sides.RemoveAt(randPoint);
                }
            }

            int randTime = Random.Range(3, 6);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }

    public IEnumerator SpawnSpoon()
    {
        List<int> sides = new List<int>{0,1,2,3};

        for(int i = 0; i <= 1; i++)
        {
            int randPoint = Random.Range(0, sides.Count);

            Instantiate(spoon, spoonSpawnPoints[sides[randPoint]], Quaternion.Euler(0, 0, 90 + (-90 * sides[randPoint])));
            sides.RemoveAt(randPoint);
        }

        yield return new WaitForSeconds(30.0f);
    }
}
