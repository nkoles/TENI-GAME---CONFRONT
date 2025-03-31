using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    GameObject[] battleUIObjects;
    TextMeshProUGUI helperText;

    public ButtonDetailHighlighting buttonController; 

    private void Awake()
    {
        ActionChosen.AddListener(SetupAction);
        ActionCompleted.AddListener(ResultOfAction);

        //ActionChosen.AddListener(buttonController.OnChoseAction);
    }

    public void SetupAction(int _actionType)
    {
        Actions actionType = (Actions)_actionType;

        switch(actionType)
        {
            case Actions.Attack:
                break;
            case Actions.Passive:
                break;
            case Actions.Affirm:
                StartAction(textControls.Affirm(dialogueRepo.healingStrings[Random.Range(0, dialogueRepo.healingStrings.Length)], 15f), actionType);
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
                GameplayManager.Instance.UpdatePlayerHealth(textControls.lastHealedHP);

                StartCoroutine(RenableBattleUI("Healed" + textControls.lastHealedHP));

                break;
            case Actions.Confront:
                break;
            case Actions.BossAction:
                break;
        }
    }
    
    private IEnumerator RenableBattleUI(string text)
    {
        foreach(var g in battleUIObjects)
        {
            g.SetActive(true);
        }

        yield return buttonController.TypeText(.3f, text, helperText);

        buttonController.OnChoosingAction();
    }

    IEnumerator StartAction(IEnumerator actionRoutine, Actions actionTag)
    {
        yield return actionRoutine;

        ActionCompleted.Invoke(actionTag);
    }
}