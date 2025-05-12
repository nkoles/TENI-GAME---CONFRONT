using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public PlayerManager player;
    public EnemyManager enemy;
    //public UIManager ui;
    public int logicAmount, emotionAmount, passiveAmount, confrontAmount;
    public int damageDealt = 0, damageTaken, aggro = 0;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn()
    {
        /*int rand = Random.Range(0, enemy.dialogueText.Length);
        ui.descriptionText.text = enemy.dialogueText[rand];*/
        
        //ui.DisplayBattleMenu();
    }

    public void Passive(int time)
    {
        //passiveAmount++;

        StartCoroutine(Build(time));
        StartCoroutine(EndTurn(time*2));
    }

    public void Logic(int time)
    {
        //logicAmount++;

        StartCoroutine(Attack(time));
        StartCoroutine(EndTurn(time*2));
    }

    public IEnumerator EndTurn(int time)
    {
        yield return new WaitForSeconds(time);

        StartTurn();
    }

    public IEnumerator Dodge(int time, int difficulty)
    {
        player.diamond.SetActive(true);
        //GameObject heart = Instantiate(player.heart, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0));
        player.heart.transform.position = new Vector3(0, 0, player.heart.transform.position.z);
        player.heart.SetActive(true);
        StartCoroutine(enemy.Dodging(time, difficulty));

        yield return new WaitForSeconds(time);

        player.heart.SetActive(false);
        //Destroy(heart);
        player.diamond.SetActive(false);
    }

    public IEnumerator Comfort(int time)
    {
        damageTaken = 0;
        aggro = 0;
        player.spade.transform.position = Input.mousePosition;
        player.spade.SetActive(true);

        yield return new WaitForSeconds(time);

        player.spade.SetActive(false);
    }

    public IEnumerator Attack(int time)
    {
        damageDealt = 0;
        damageTaken = 0;
        player.diamond.SetActive(true);
        StartCoroutine(enemy.Blocking(time));

        yield return new WaitForSeconds(time);

        //ui.dialogueBox.SetActive(false);
        player.diamond.SetActive(false);
        //StartCoroutine(Dodge(time, logicAmount));
    }

    public IEnumerator Build(int time)
    {
        player.club.SetActive(true);

        yield return new WaitForSeconds(time);

        player.club.SetActive(false);
    }

    public void UpdatePlayerHealth(int amount)
    {
        if(!player.invulnerable || amount < 0)
        {
            player.hp -= amount;
            Debug.Log("Player HP: " + player.hp);

            if(player.hp <= 0)
            {
                Lose();
            }
            else if(player.hp > player.maxHP)
            {
                player.hp = player.maxHP;
            }
            else if(amount > 0)
            {
                player.StartCoroutine(player.Invulnerable(amount));
            }

            HealthManager.instance.UpdateUIHealth(true);
        }
    }

    public void UpdateEnemyHealth(int amount)
    {
        enemy.hp -= amount;
        Debug.Log("Enemy HP: " + enemy.hp);

        if(enemy.hp <= 0)
        {
            Win();
        }
        else if(enemy.hp > enemy.maxHP)
        {
            enemy.hp = enemy.maxHP;
        }

        HealthManager.instance.UpdateUIHealth(false);
    }

    public void UpdateAggression(int amount)
    {
        player.aggression += amount;

        if(player.aggression > player.maxAggression)
        {
            player.aggression = player.maxAggression;
        }
    }

    public void Win()
    {
        SetupSequence.isWon = true;
        SceneLoadingManager.instance.LoadNextScene();
    }

    public void Lose()
    {
        SetupSequence.isWon = false;
        SceneLoadingManager.instance.LoadNextScene();
    }
}
