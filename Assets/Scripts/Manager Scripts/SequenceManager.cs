using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    public TextMeshProUGUI helperText;

    public ButtonDetailHighlighting buttonController; 

    private void Awake()
    {
        ActionChosen.AddListener(SetupAction);
        ActionCompleted.AddListener(ResultOfAction);
    }

    public void SetupAction(int _actionType)
    {
        Actions actionType = (Actions)_actionType;

        buttonController.OnChoseAction(_actionType);

        print("action invoked");

        IEnumerator action = null;

        switch(actionType)
        {
            case Actions.Attack:
                break;
            case Actions.Passive:
                print("Passive Action");

                action = textControls.Attack(dialogueRepo.passiveStrings, textControls.spawnBox, Screen.width/2, 2, 30);

                break;
            case Actions.Affirm:
                print("Affirm Action");

                action = textControls.Affirm(dialogueRepo.healingStrings[Random.Range(0, dialogueRepo.healingStrings.Length)], 15f);

                break;
            case Actions.Confront:
                break;
            case Actions.BossAction:
                break;
        }

        StartCoroutine(StartAction(action , actionType));
    }
    
    public void ResultOfAction(Actions action)
    {
        string resultText = "";

        switch (action)
        {
            case Actions.Attack:
                resultText = "Dealt Damge: " + "";
                break;
            case Actions.Passive:
                resultText = "Endured Damage: " + "";
                break;
            case Actions.Affirm:
                resultText = "Healed: " + textControls.lastHealedHP + "HP" + "\nPress any key to Continue";
                break;
            case Actions.Confront:
                break;
            case Actions.BossAction:
                break;
        }

        StartCoroutine(RenableBattleUI(resultText, action));
    }
    
    private IEnumerator RenableBattleUI(string text, Actions action)
    {
        actionUI[(int)action].SetActive(false);
        bossUI.SetActive(true);

        foreach(var t in battleUIObjects.GetComponentsInChildren<Transform>())
        {
            t.gameObject.SetActive(true);
        }

        yield return buttonController.TypeText(.3f, text, helperText);
        yield return WaitForInput();

        yield return new WaitForSeconds(.5f);
        buttonController.OnChoosingAction();
    }

    IEnumerator StartAction(IEnumerator actionRoutine, Actions actionTag)
    {
        print("Starting Action");

        yield return actionRoutine;

        ActionCompleted.Invoke(actionTag);
    }

    IEnumerator WaitForInput()
    {
        while (!Input.anyKeyDown || !Input.GetMouseButtonDown(0))
            yield return null;
    }
}