using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    public GameObject book;
    public float time;
    public List<Vector3> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBook());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnBook()
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

                    Instantiate(book, spawnPoints[sides[randPoint]], Quaternion.Euler(0, 0, -180 + (-90 * sides[randPoint])));
                    sides.RemoveAt(randPoint);
                }
            }

            int randTime = Random.Range(1, 5);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }
}
