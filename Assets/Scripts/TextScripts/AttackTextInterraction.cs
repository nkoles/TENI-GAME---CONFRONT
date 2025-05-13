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
                if(destroyedCharIdx.Count < m_TextAsset.textInfo.wordCount)
                {
                    int start = m_TextAsset.textInfo.wordInfo[destroyedCharIdx.Count].firstCharacterIndex;
                    int end = m_TextAsset.textInfo.wordInfo[destroyedCharIdx.Count].lastCharacterIndex;

                    for(int i = start; i <= end; i++)
                    {
                        StartCoroutine(TextControls.LerpCharColor(m_TextAsset, i, TextControls.GetCharColor(m_TextAsset, i), Color.clear, .3f));
                    }

                    destroyedCharIdx.Add(destroyedCharIdx.Count);
                }
                
                //randomChar = TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, null, true);

                if (destroyedCharIdx.Count >= m_TextAsset.textInfo.wordCount)
                {
                    isDestroyed = true;

                    if (_destructionRoutine == null)
                        _destructionRoutine = StartCoroutine(OnDestruction());

                    return;
                }
            }
        }
    }

    private void OnCursorOffText()
    {
        Select(false);

        isEntered = false;
    }

    public void Initialise(Vector3 direction, TMP_FontAsset font, float _speed)
    {
        print(m_TextAsset.text);

        m_TextAsset.font = font;

        parsedString = TextControls.ParseRichTags(m_TextAsset);
        _dir = direction;

        //print(_dir);

        speed = _speed;

        isInitialised = true;

        print("entered");
    }

    public bool CheckMouseOverText()
    {
        if (TMP_TextUtilities.FindIntersectingCharacter(m_TextAsset, Input.mousePosition, Camera.main, true) != -1)
            return true;

        return false;
    }

    private bool CheckOutsideScreen()
    {
        //print(Screen.width);
        //print(m_RectTransform.localPosition);

        bool screenX = m_RectTransform.localPosition.x < -Screen.width/2 || m_RectTransform.localPosition.x > Screen.width/2;
        bool screenY = m_RectTransform.localPosition.y < -Screen.height/2 || m_RectTransform.localPosition.y > Screen.height/2;

        //print(screenY || screenX);

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
            transform.position += _dir/2 * Time.deltaTime * speed;

            transform.localScale += Vector3.one*Time.deltaTime*speed;
        }

        if (CheckOutsideScreen() && !isDestroyed)
        {            
            isDestroyed = true;
            TextControls textController = FindAnyObjectByType<TextControls>();

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
        print(destroyedCharIdx.Count);
        print(m_TextAsset.textInfo.wordCount);

        Color colorToChangeTo = Color.white;

        if (isOver)
            colorToChangeTo = Color.magenta;

        for(int i = destroyedCharIdx.Count; i < m_TextAsset.textInfo.wordCount; ++i)
        {
            int start = m_TextAsset.textInfo.wordInfo[i].firstCharacterIndex;
            int last = m_TextAsset.textInfo.wordInfo[i].lastCharacterIndex;

            for (int j = start; j <= last; ++j)
            {
                TextControls.ChangeCharColor(m_TextAsset, j, colorToChangeTo);
            }
        }
    }

    public IEnumerator OnDestruction()
    {
        TextControls textController = FindAnyObjectByType<TextControls>();

        if(textController.currentActiveWords.Contains(this))
        {
            textController.currentActiveWords.Remove(this);

            if(destroyedCharIdx.Count >= m_TextAsset.textInfo.wordCount)
            {
                GameplayManager.Instance.UpdateAggression(m_TextAsset.textInfo.wordCount);
                textController.aggressionGained += m_TextAsset.textInfo.wordCount;
            } else
            {
                GameplayManager.Instance.UpdatePlayerHealth(m_TextAsset.textInfo.wordCount - destroyedCharIdx.Count);
                textController.damageTaken += m_TextAsset.textInfo.wordCount - destroyedCharIdx.Count;
            }

            textController.wordsDestroyedCount++;
        }

        //yield return TextControls.LerpCharColor(m_TextAsset, parsedString.Length - 1, TextControls.GetCharColor(m_TextAsset, parsedString.Length - 1), Color.clear, 0.1f);

        Destroy(gameObject);

        //StartCoroutine(PostProcessingManager.TakeDamagePPEffect(.3f, 1f, 1f));

        yield return null;
    }
}
