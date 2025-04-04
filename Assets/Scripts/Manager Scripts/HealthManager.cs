using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    static public HealthManager instance;

    public Image playerHealthImage;
    public Image bossHealthImage;

    public EnemyManager enemyManager;
    public PlayerManager playerManager;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateUIHealth(bool isPlayer)
    {
        if (!isPlayer)
        {
            bossHealthImage.fillAmount = Mathf.Lerp(0, 1, enemyManager.hp/enemyManager.maxHP);
        } else
        {
            playerHealthImage.fillAmount = Mathf.Lerp(0, 1, playerManager.hp / playerManager.maxHP);
        }
    }
}
