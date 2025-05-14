using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text text;
    public string[] descriptions;
    public GameObject battleUI;
    private int tutorialAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActionTutorial(int action)
    {
        text.gameObject.SetActive(true);
        text.text = descriptions[action];
        buttons[action].gameObject.SetActive(true);
        buttons[action].enabled = true;
    }

    public void RemoveButton(GameObject deactivated)
    {
        deactivated.GetComponent<Image>().enabled = false;
        deactivated.GetComponent<Button>().interactable = false;
        Destroy(deactivated.GetComponent<ButtonCheckHover>());
    }

    public void BuildAggression()
    {
        GameplayManager.Instance.player.aggression = GameplayManager.Instance.player.maxAggression;
    }

    public void TutorialsSeen()
    {
        text.gameObject.SetActive(false);
        tutorialAmount++;

        if(tutorialAmount == descriptions.Length)
        {
            GameplayManager.Instance.Win();
            battleUI.SetActive(true);
        }
    }
}
