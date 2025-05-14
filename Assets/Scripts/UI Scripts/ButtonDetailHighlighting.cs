using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonDetailHighlighting : MonoBehaviour
{
    static public UnityEvent<int> ButtonSelected = new UnityEvent<int>();
    static public UnityEvent ButtonDeselected = new UnityEvent();
    static public ButtonDetailHighlighting instance;

    public TextMeshProUGUI buttonDescriptionTMProTarget;
    public float textSpeed;

    private int _lastActivatedTextID;
    private Coroutine _typingRoutine;

    [Header("0 = Attack, 1 = Passive, 2 = Affirm, 3 = Confront")]
    public string[] buttonDescription;

    public Button[] buttons;

    public Vector3 initPos;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        buttons = GetComponentsInChildren<Button>();

        initPos = transform.position;

        OnChoosingAction();
    }

    public IEnumerator MoveButtonOutOfView(bool isView, float speed = 1f)
    {
        Vector3 target = initPos + Vector3.down * 1;

        if (isView)
            target = initPos;

        for(float i = 0; i < 1; i += Time.fixedDeltaTime/speed)
        {
            if(i + Time.fixedDeltaTime/speed > 1)
            {
                transform.position = target;
                break;
            }

            transform.position = Vector3.Lerp(transform.position, target, i);

            yield return null;
        }
    }

    public void MoveButtonOutOfView(bool isView)
    {
        StartCoroutine(MoveButtonOutOfView(isView, 1f));
    }

    public void OnChoosingAction()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        buttonDescriptionTMProTarget.gameObject.SetActive(true);
        buttonDescriptionTMProTarget.text = "";

        ButtonSelected.AddListener(OnButtonSelected);
        ButtonDeselected.AddListener(OnButtonDeselected);
    }

    public void OnChoseAction(int actionID)
    {
        ButtonSelected.RemoveListener(OnButtonSelected);
        ButtonDeselected.RemoveListener(OnButtonDeselected);

        foreach(Button button in buttons)
        {
            button.interactable = false;
        }

        //StartCoroutine(MoveDecisionsAway(true));
    }

    public void OnButtonDeselected()
    {
        StopAllCoroutines();
        _typingRoutine = null;

        buttonDescriptionTMProTarget.text = "";
    } 

    private void OnButtonSelected(int buttonID)
    {
        if(_typingRoutine == null)
        {
            _typingRoutine = StartCoroutine(TypeText(textSpeed, buttonDescription[buttonID], buttonDescriptionTMProTarget));
        }
    }

    public IEnumerator TypeText(float speed, string text, TextMeshProUGUI tmproAsset)
    {
        tmproAsset.text = "";

        foreach(var c in text)
        {
            tmproAsset.text += c;

            yield return new WaitForSeconds(speed / text.Length);
        }

        _typingRoutine = null;
    }

    private IEnumerator MoveDecisionsAway(bool isAway)
    {
        Vector3 dir = Vector3.down * 100;

        if(!isAway)
        {
            dir = -dir;
        }

        for(float i = 0; i < 1.1; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + dir, i);

            yield return null;
        }
    }
}
