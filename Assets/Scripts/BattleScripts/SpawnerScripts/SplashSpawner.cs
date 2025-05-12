using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSpawner : MonoBehaviour
{
    public GameObject water;
    public float time = 30.0f;

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
            float randPos = Random.Range(-3, 3);
            float randRot = Random.Range(0, 360);

            Instantiate(water, new Vector3(randPos, transform.position.y, 0), Quaternion.Euler(0, 0, randRot));

            int randTime = Random.Range(0, 4);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }
}
