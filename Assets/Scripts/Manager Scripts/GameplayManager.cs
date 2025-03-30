using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public PlayerManager player;
    public EnemyManager enemy;
    public UIManager ui;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        
        ui.DisplayBattleMenu();
    }

    public void Passive(int time)
    {
        StartCoroutine(Dodge(time));
        StartCoroutine(Block(time));
        StartCoroutine(EndTurn(time));
    }

    public IEnumerator EndTurn(int time)
    {
        yield return new WaitForSeconds(time);

        StartTurn();
    }

    public IEnumerator Dodge(int time)
    {
        player.heart.transform.position = new Vector3(0, 0, player.heart.transform.position.z);
        player.heart.SetActive(true);

        yield return new WaitForSeconds(time);

        player.heart.SetActive(false);
    }

    public IEnumerator Counter(int time)
    {
        player.spade.transform.position = Input.mousePosition;
        player.spade.SetActive(true);

        yield return new WaitForSeconds(time);

        player.spade.SetActive(false);
    }

    public IEnumerator Block(int time)
    {
        player.diamond.SetActive(true);
        StartCoroutine(enemy.Blocking(time));

        yield return new WaitForSeconds(time);

        player.diamond.SetActive(false);
    }

    public IEnumerator Attack(int time)
    {
        player.club.SetActive(true);

        yield return new WaitForSeconds(time);

        player.club.SetActive(false);
    }

    public void UpdatePlayerHealth(int amount)
    {
        if(!player.invulnerable)
        {
            player.hp -= amount;

            if(player.hp <= 0)
            {
                Lose();
            }
            else if(player.hp > player.maxHP)
            {
                player.hp = player.maxHP;
            }
            else
            {
                player.StartCoroutine(player.Invulnerable());
            }
        }
    }

    public void UpdateEnemyHealth(int amount)
    {
        enemy.hp -= amount;

        if(enemy.hp <= 0)
        {
            Win();
        }
        else if(enemy.hp > enemy.maxHP)
        {
            enemy.hp = enemy.maxHP;
        }
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

    }

    public void Lose()
    {

    }
}
