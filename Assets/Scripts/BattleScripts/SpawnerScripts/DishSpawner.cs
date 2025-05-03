using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    public GameObject dish;
    public float time;

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
        while(tempTime > 8)
        {
            transform.position = Random.insideUnitCircle.normalized * 5;
            transform.rotation = Quaternion.FromToRotation(transform.right, (new Vector3(0,0,0) - transform.position));
            Instantiate(dish, transform.position, transform.rotation);

            int randTime = Random.Range(3, 6);

            yield return new WaitForSeconds(randTime);

            tempTime -= randTime;
        }
    }
}
