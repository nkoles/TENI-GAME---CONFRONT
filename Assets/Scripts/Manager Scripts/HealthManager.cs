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

        UpdateUIHealth(true);
        UpdateUIHealth(false);
    }

    public void UpdateUIHealth(bool isPlayer)
    {
        if (!isPlayer)
        {
            bossHealthImage.fillAmount = Mathf.Lerp(0, 1, (float)enemyManager.hp/(float)enemyManager.maxHP);
        } else
        {
            playerHealthImage.fillAmount = Mathf.Lerp(0, 1, (float)playerManager.hp / (float)playerManager.maxHP);
        }
    }
}
