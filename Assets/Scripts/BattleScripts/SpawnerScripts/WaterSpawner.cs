using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject water;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWater());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnWater()
    {
        float tempTime = time;
        while(tempTime > 8)
        {
            Instantiate(water, transform.position, transform.rotation);

            int randTime = Random.Range(0, 4);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }
}
