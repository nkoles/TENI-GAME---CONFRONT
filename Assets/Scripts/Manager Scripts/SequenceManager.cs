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

        switch(actionType)
        {
            case Actions.Attack:
                break;
            case Actions.Passive:
                break;
            case Actions.Affirm:
                print("Affirm Action");

                StartCoroutine(StartAction(textControls.Affirm(dialogueRepo.healingStrings[Random.Range(0, dialogueRepo.healingStrings.Length)], 15f), actionType));
                break;
            case Actions.Confront:
                break;
            case Actions.BossAction:
                break;
        }
    }
    
    public void ResultOfAction(Actions action)
    {


        switch (action)
        {
            case Actions.Attack:
                break;
            case Actions.Passive:
                break;
            case Actions.Affirm:
                //GameplayManager.Instance.UpdatePlayerHealth(textControls.lastHealedHP);
                StartCoroutine(RenableBattleUI("Healed: " + textControls.lastHealedHP + "HP" + "\n Press any key to Continue", action));

                break;
            case Actions.Confront:
                break;
            case Actions.BossAction:
                break;
        }
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

        yield return new WaitForSeconds(2);

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
        while (!Input.anyKeyDown)
            yield return null;
    }
}