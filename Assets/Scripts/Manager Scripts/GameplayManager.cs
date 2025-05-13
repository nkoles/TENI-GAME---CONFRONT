using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public PlayerManager player;
    public EnemyManager enemy;
    public SequenceManager sequence;
    public DialogueManager dialogue;
    public AudioManager audioManager;
    //public DiamondScript[] diamonds;
    //public UIManager ui;
    public int logicAmount, emotionAmount, passiveAmount, confrontAmount;
    public int damageDealt = 0, damageTaken, aggro = 0;
    public string tempObstacle;
    public UnityAction Defeat;

    public Material confrontButtonShader;
    public Material defaultButton;
    public GameObject confrontButton;

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

    private void Start()
    {
        UpdateAggression(0);
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

        //StartCoroutine(Attack());
        StartCoroutine(EndTurn(time*2));
    }

    public IEnumerator EndTurn(int time)
    {
        yield return new WaitForSeconds(time);

        StartTurn();
    }

    public IEnumerator Dodge(int time, int difficulty)
    {
        damageTaken = 0;
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

    public IEnumerator Attack(int amount)
    {
        damageDealt = 0;
        damageTaken = 0;

        /*foreach(DiamondScript diamond in diamonds)
        {
            diamond.gameObject.GetComponent<PlayerInput>().enabled = false;
        }*/

        player.diamond.SetActive(true);

        yield return enemy.Blocking(amount);

        /*StartCoroutine(enemy.Blocking(time));

        yield return new WaitForSeconds(time);*/

        //ui.dialogueBox.SetActive(false);
        player.diamond.SetActive(false);
        //StartCoroutine(Dodge(time, logicAmount));

        /*foreach(DiamondScript diamond in diamonds)
        {
            diamond.gameObject.GetComponent<PlayerInput>().enabled = true;
        }*/
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
                isLose = true;
            }
            else if(player.hp > player.maxHP)
            {
                player.hp = player.maxHP;
            }
            else if(amount > 0)
            {
                player.StartCoroutine(player.Invulnerable(amount));
            }

            if(amount > 0)
            {
                PostProcessingManager.instance.StopAllCoroutines();
                StartCoroutine(PostProcessingManager.instance.TakeDamagePPEffect(.25f, 3f, 0f));
            }

            HealthManager.instance.UpdateUIHealth(true);
        }
    }

    public void UpdateEnemyHealth(int amount)
    {
        enemy.hp -= amount;
        Debug.Log("Enemy HP: " + enemy.hp);

        if((enemy.hp == 0) && (sequence.bossAttacking == false))
        {
            isWin = true;
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

        confrontButtonShader.SetFloat("_Strength", (float)player.aggression / (float)player.maxAggression);

        //if (player.aggression == player.maxAggression)
        //{
        //    confrontButton.GetComponent<Image>().material = confrontButtonShader;
        //}
        //else
        //{
        //    confrontButton.GetComponent<Image>().material = defaultButton;
        //}
    }

    public bool isWin;
    public bool isLose;

    public void Win()
    {
        ButtonDetailHighlighting.instance.MoveButtonOutOfView(false);

        SetupSequence.isWon = true;
        if (enemy.phase == 0)
        {
            audioManager.friendStart = false;
            audioManager.friendEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
        else if (enemy.phase == 1)
        {
            audioManager.principalStart = false;
            audioManager.principalGoodEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
        else if (enemy.phase == 2)
        {
            audioManager.auntStart = false;
            audioManager.auntGoodEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
        else if (enemy.phase == 3)
        {
            audioManager.doctorStart = false;
            audioManager.doctorGoodEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
    }

    public void Lose()
    {
        ButtonDetailHighlighting.instance.MoveButtonOutOfView(false);

        SetupSequence.isWon = false;
        if (enemy.phase == 1)
        {
            audioManager.principalStart = false;
            audioManager.principalBadEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
        else if (enemy.phase == 2)
        {
            audioManager.auntStart = false;
            audioManager.auntBadEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
        else if (enemy.phase == 3)
        {
            audioManager.doctorStart = false;
            audioManager.doctorBadEnd = true;
            audioManager.battleEnd = true;
            dialogue.SceneMusic();
            dialogue.StartDialogue();
        }
    }
}
