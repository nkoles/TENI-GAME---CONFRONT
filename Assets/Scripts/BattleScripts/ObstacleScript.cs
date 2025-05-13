using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameplayManager.Instance.player.heart.activeSelf != true)
        {
            Destroy(gameObject);
        }
    }
}
