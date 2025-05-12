using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    public GameObject[] dishes;
    public float time;
    public int minDelay, maxDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDish());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnDish()
    {
        float tempTime = time;
        int randNo = Random.Range(0, dishes.Length);

        if(tempTime > 8)
        {
            transform.position = Random.insideUnitCircle.normalized * 5;
            transform.rotation = Quaternion.FromToRotation(transform.right, (new Vector3(0,0,0) - transform.position));
            Instantiate(dishes[randNo], transform.position, transform.rotation);

            int randTime = Random.Range(minDelay, maxDelay);

            yield return new WaitForSeconds(randTime);

            time -= randTime;

            StartCoroutine(SpawnDish());
        }
    }
}
