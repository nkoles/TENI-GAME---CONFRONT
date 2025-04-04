using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public float invulnerableTime = 1.0f;

    public int hp, maxHP, aggression, maxAggression;

    public bool invulnerable;
    
    public GameObject heart, diamond, spade, club;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHP;
        aggression = 0;
    }

    public IEnumerator Invulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <=0)
        {
            //GameplayManager.Instance.logicAmount = 0;
            //GameplayManager.Instance.emotionAmount = 0;
            //GameplayManager.Instance.passiveAmount = 0;
            //GameplayManager.Instance.confrontAmount = 0;
        }
    }
}
