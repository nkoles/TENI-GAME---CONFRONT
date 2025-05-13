using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.Defeat += Death;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDisable()
    {
        GameplayManager.Instance.Defeat -= Death;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
