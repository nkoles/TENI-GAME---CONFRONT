using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable] 
public enum Actions
{
    Attack = 0,
    Passive = 1,
    Affirm = 2,
    Confront = 3,
    BossAction = 4  
}

public class SequenceManager : MonoBehaviour
{
    public UnityEvent<int> ActionChosen = new UnityEvent<int>();
    public UnityEvent<Actions> ActionCompleted = new UnityEvent<Actions>();

    public DialogueDictionnary dialogueRepo;

    public TextControls textControls;

    public GameObject battleUIObjects;
    public GameObject bossUI;
    public GameObject[] actionUI;
    public bool bossAttacking = false;
    public DialogueManager dialogue;
    public AudioManager audioManager;

    public TextMeshProUGUI helperText;

    public ButtonDetailHighlighting buttonController;
    public EnemyManager enemy;

    private Actions lastAction;
    private int[] actionAmount = new int[5] {0,0,0,0,0};

    private void Awake()
    {
        enemy = GameplayManager.Instance.enemy;
        ActionChosen.AddListener(SetupAction);
        ActionCompleted.AddListener(ResultOfAction);
    }

    public void SetupAction(int _actionType)
    {
        Actions actionType = (Actions)_actionType;

        buttonController.OnChoseAction(_actionType);

        print("action invoked");

        IEnumerator action = null;
        
        switch (actionType)
        {
            case Actions.Attack:
                print("Attack Action");

                action = GameplayManager.Instance.Attack(actionAmount[0] + 1);

                //GameplayManager.Instance.logicAmount++;
                break;
            case Actions.Passive:
                print("Passive Action");

                GameplayManager.Instance.aggro = 0;

                List<string> randomString = new List<string>();

                int passiveButtonClick = GameplayManager.Instance.passiveAmount;
                int phase_ = EnemyManager.instance.phase;

                int textAmount = 1 + passiveButtonClick;
                int spawnTime = 5 - passiveButtonClick;
                float speed = 1 + phase_ / 2;

                if (passiveButtonClick >= dialogueRepo.passiveStrings.Length)
                    textAmount = dialogueRepo.passiveStrings.Length;

                for(int i =0; i < textAmount; ++i)
                {
                    int randomIdx = Random.Range(0, dialogueRepo.passiveStrings.Length);

                    while (randomString.Contains(dialogueRepo.passiveStrings[randomIdx]))
                    {
                        randomIdx = Random.Range(0, dialogueRepo.passiveStrings.Length);
                    }

                    randomString.Add(dialogueRepo.passiveStrings[randomIdx]);
                }

                action = textControls.Attack(randomString.ToArray(), textControls.spawnBox, Screen.width/2, 3, 20, speed);

                GameplayManager.Instance.passiveAmount++;
                break;
            case Actions.Affirm:
                print("Affirm Action");

                //EnemyManager.instance.phase;

                int phase = EnemyManager.instance.phase;
                int buttonClicked = GameplayManager.Instance.emotionAmount;

                float typingTime = typingTime = 30;
                int dialogueID = buttonClicked;


                if (phase != 0)
                    typingTime /= phase;

                if (buttonClicked >= dialogueRepo.healingStrings.Length)
                    dialogueID = dialogueRepo.healingStrings.Length-1;

                action = textControls.Affirm(dialogueRepo.healingStrings[dialogueID], typingTime);

                GameplayManager.Instance.emotionAmount++;
                break;
            case Actions.Confront:
                print("Confront Action");

                action = Confront(30);

                GameplayManager.Instance.UpdateAggression(-GameplayManager.Instance.player.maxAggression);

                break;
            case Actions.BossAction:

                action = GameplayManager.Instance.Dodge(30, actionAmount[(int)lastAction]);
                break;
        }

        lastAction = actionType;
        actionAmount[(int)lastAction]++;

        if(GameplayManager.Instance.enemy.phase == 0 && _actionType == 4 && actionAmount[(int)lastAction] == 1)
        {
            GameplayManager.Instance.DodgeTutorial(30, 1);
        }
        else if( _actionType != 3)
        {
            StartCoroutine(StartAction(action , actionType));
        }
    }

    public IEnumerator Confront(int time)
    {
        // OLD CONFRONT ACTION

        /*GameplayManager.Instance.player.damage *= 2;
        Debug.Log(GameplayManager.Instance.player.damage);
        textControls.aggressionAmount *= 2;
        Debug.Log(textControls.aggressionAmount);

        StartCoroutine(GameplayManager.Instance.Attack(time));
        StartCoroutine(textControls.Attack(dialogueRepo.passiveStrings, textControls.spawnBox, Screen.width/2, 2, time));
        StartCoroutine(textControls.Affirm(dialogueRepo.healingStrings[Random.Range(0, dialogueRepo.healingStrings.Length)], time));*/

        // NEW CONFRONT ACTION

        yield return new WaitForSeconds(time);

        /*foreach(GameObject ui in actionUI)
        {
            ui.SetActive(false);
        }*/

        //GameplayManager.Instance.player.damage /= 2;
        //textControls.aggressionAmount /= 2;
    }

    public void SettupConfront()
    {
        lastAction = Actions.Confront;
        ResultOfAction(Actions.Confront);
    }

    public void ResultOfAction(Actions action)
    {
        StartCoroutine(buttonController.MoveButtonOutOfView(true, 1f));

        string resultText = "";

        switch (action)
        {
            case Actions.Attack:
                resultText = "Dealt: " + GameplayManager.Instance.damageDealt + " Damage" +
                "\nTook: " + GameplayManager.Instance.damageTaken + " Damage" +
                "\nPress any key to Continue";

                break;
            case Actions.Passive:
                resultText = "Recieved: " + textControls.aggressionGained + " Aggression" +
                "\nTook: " + textControls.damageTaken + " Damage" +
                "\nPress any key to Continue";
                break;
            case Actions.Affirm:
                resultText = "Gained: " + textControls.lastHealedHP + " Health" + "\nPress any key to Continue";
                break;
            case Actions.Confront:
                resultText = "Disarmed their " + GameplayManager.Instance.tempObstacle + "." +
                "\nPress any key to Continue";
                break;
            case Actions.BossAction:
                bossAttacking = false;
                resultText = "Took " + GameplayManager.Instance.damageTaken + " damage." +
                "\nPress any key to Continue";
                break;
        }

        StartCoroutine(RenableBattleUI(resultText, action));
    }
    
    private IEnumerator RenableBattleUI(string text, Actions action)
    {
        actionUI[(int)action].SetActive(false);
        bossUI.SetActive(true);

        foreach(Button button in buttonController.gameObject.GetComponentsInChildren<Button>())
        {
            button.enabled = false;
        }

        foreach(var t in battleUIObjects.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.SetActive(true);
        }
        
        if(text != "")
        {
            yield return buttonController.TypeText(.3f, text, helperText);
            yield return WaitForInput();
            yield return new WaitForSeconds(.5f);
        }

        if (GameplayManager.Instance.isWin || GameplayManager.Instance.isLose)
        {
            if (GameplayManager.Instance.isWin)
            {
                GameplayManager.Instance.Win();
            }

            if (GameplayManager.Instance.isLose)
            {
                GameplayManager.Instance.Lose();
            }
        } else
        {
            if (lastAction != Actions.BossAction && lastAction != Actions.Confront)
            {
                bossUI.SetActive(false);

                foreach (var t in battleUIObjects.GetComponentsInChildren<Transform>())
                {
                    t.gameObject.SetActive(false);
                }

                SetupAction((int)Actions.BossAction);
                bossAttacking = true;
            }
            else
            {
                int buttonNo = 0;

                foreach (Button button in buttonController.gameObject.GetComponentsInChildren<Button>())
                {
                    if (buttonNo == 3 && GameplayManager.Instance.player.aggression < GameplayManager.Instance.player.maxAggression)
                    {
                        button.enabled = false;
                    }
                    else
                    {
                        button.enabled = true;
                    }

                    buttonNo++;
                }

                buttonController.OnChoosingAction();
            }
        }
    }

    IEnumerator StartAction(IEnumerator actionRoutine, Actions actionTag)
    {
        print("Starting Action");

        yield return actionRoutine;

        ActionCompleted.Invoke(actionTag);
    }

    public void DodgeTutorial()
    {
        IEnumerator action = null;

        action = GameplayManager.Instance.Dodge(30, 1);

        StartCoroutine(StartAction(action, (Actions)4));
    }

    IEnumerator WaitForInput()
    {
        while (!Input.anyKeyDown || !Input.GetMouseButtonDown(0))
            yield return null;
    }
}