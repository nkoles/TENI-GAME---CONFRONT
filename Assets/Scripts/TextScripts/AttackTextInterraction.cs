using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AttackTextInterraction : MonoBehaviour//, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEvent CursorOverText = new UnityEvent();
    private UnityEvent CursorOffText  = new UnityEvent();

    private RectTransform m_RectTransform;

    private bool isEntered;

    private TextMeshProUGUI m_TextAsset;

    private string parsedString;

    public float speed = .5f;

    private List<int> destroyedCharIdx = new List<int>();

    private bool isInitialised;
    private bool isDestroyed;

    private Coroutine _destructionRoutine;

    private Vector3 _dir;

    private void Awake()
    {
        m_TextAsset = GetComponent<TextMeshProUGUI>();
        m_TextAsset.alignment = TextAlignmentOptions.Center & TextAlignmentOptions.Center;
        
        m_RectTransform = GetComponent<RectTransform>();

        CursorOverText.AddListener(OnCursorOverText);
        CursorOffText.AddListener(OnCursorOffText);
    }

    private void OnCursorOverText()
    {
        isEntered = true;

        Select(true);

        //print(TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true));
        //print(parsedString[TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true)]);

        //SelectChar(true, TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true));

        if (Input.GetMouseButtonDown(0))
        {
            if (!isDestroyed)
            {
                int randomChar = 0;

                for (int i = 0; i < parsedString.Length; ++i)
                {
                    if (!destroyedCharIdx.Contains(i))
                    {
                        randomChar = i;

                        break;
                    }
                }

                //randomChar = TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true);

                destroyedCharIdx.Add(randomChar);

                if (destroyedCharIdx.Count >= parsedString.Length)
                {
                    isDestroyed = true;

                    if (_destructionRoutine == null)
                        _destructionRoutine = StartCoroutine(OnDestruction());

                    return;
                }

                StartCoroutine(TextControls.LerpCharColor(m_TextAsset, randomChar, TextControls.GetCharColor(m_TextAsset, randomChar), Color.clear, 0.5f));
            }
        }
    }

    private void OnCursorOffText()
    {
        Select(false);

        isEntered = false;
    }

    public void Initialise(Vector3 direction)
    {
        print(m_TextAsset.text);

        parsedString = TextControls.ParseRichTags(m_TextAsset);
        _dir = direction;

        //print(_dir);

        isInitialised = true;

        print("entered");
    }

    public bool CheckMouseOverText()
    {
        if (TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true) != -1)
            return true;

        return false;
    }

    private bool CheckOutsideScreen()
    {
        print(Screen.width);
        print(m_RectTransform.localPosition);

        bool screenX = m_RectTransform.localPosition.x < -Screen.width/2 || m_RectTransform.localPosition.x > Screen.width/2;
        bool screenY = m_RectTransform.localPosition.y < -Screen.height/2 || m_RectTransform.localPosition.y > Screen.height/2;

        print(screenY || screenX);

        return screenX || screenY;
    }

    private void Update()
    {
        if (CheckMouseOverText())
            CursorOverText.Invoke();
        else if (!CheckMouseOverText())
            CursorOffText.Invoke();

        if (isInitialised)
        {
            transform.position += _dir * 20 * Time.deltaTime;

            for (int i = 0; i < parsedString.Length; ++i)
            {
                TextControls.ChangeCharFontSize(m_TextAsset, i, TextControls.GetCharFontSize(m_TextAsset, i) + 10 * Time.deltaTime);
            }
        }

        if (CheckOutsideScreen() && !isDestroyed)
        {            
            isDestroyed = true;
            TextControls textController = FindAnyObjectByType<TextControls>();

            textController.wordsMissedCount++;

            StartCoroutine(OnDestruction());
        }
    }

    //private void SelectChar(bool isOver, int idx)
    //{
    //    Color colorToChangeTo = Color.white;

    //    if (isOver)
    //        colorToChangeTo = Color.magenta;
        
        

    //    TextControls.ChangeCharColor(m_TextAsset, idx, colorToChangeTo);
    //    TextControls.ChangeCharFontSize(m_TextAsset, idx, TextControls.GetCharFontSize(m_TextAsset, idx) * 1.2f);
    //}

    private void Select(bool isOver)
    {
        Color colorToChangeTo = Color.white;

        if (isOver)
            colorToChangeTo = Color.magenta;

        for(int i = 0; i < parsedString.Length; ++i)
        {
            if(!destroyedCharIdx.Contains(i))
                TextControls.ChangeCharColor(m_TextAsset, i, colorToChangeTo);
        }
    }

    public IEnumerator OnDestruction()
    {
        TextControls textController = FindAnyObjectByType<TextControls>();

        if(textController.currentActiveWords.Contains(this))
        {
            textController.currentActiveWords.Remove(this);
            textController.wordsDestroyedCount++;
        }

        yield return TextControls.LerpCharColor(m_TextAsset, parsedString.Length - 1, TextControls.GetCharColor(m_TextAsset, parsedString.Length - 1), Color.clear, 0.1f);

        Destroy(gameObject);

        yield return null;
    }
}
