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

    public TextMeshProUGUI buttonDescriptionTMProTarget;
    public float textSpeed;

    private int _lastActivatedTextID;
    private Coroutine _typingRoutine;

    [Header("0 = Attack, 1 = Passive, 2 = Affirm, 3 = Confront")]
    public string[] buttonDescription;

    public void Awake()
    {
        OnChoosingAction();
    }

    public void OnChoosingAction()
    {
        buttonDescriptionTMProTarget.gameObject.SetActive(true);
        buttonDescriptionTMProTarget.text = "";

        ButtonSelected.AddListener(OnButtonSelected);
        ButtonDeselected.AddListener(OnButtonDeselected);
    }

    public void OnChoseAction(int actionID)
    {
        ButtonSelected.RemoveListener(OnButtonSelected);
        ButtonDeselected.RemoveListener(OnButtonDeselected);

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
