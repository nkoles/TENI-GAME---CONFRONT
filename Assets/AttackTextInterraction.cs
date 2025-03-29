using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackTextInterraction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI m_TextAsset;

    private string parsedString;

    public float speed = .5f;

    private List<int> destroyedCharIdx = new List<int>();

    private bool isInitialised;

    private void Awake()
    {
        m_TextAsset = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if(destroyedCharIdx.Count >= parsedString.Length)
        {
            OnDestruction();
        }

        int randomChar = Random.Range(0, parsedString.Length);

        while(destroyedCharIdx.Contains(randomChar))
        {
            randomChar = Random.Range(0, parsedString.Length);
        }

        destroyedCharIdx.Add(randomChar);

        StartCoroutine(TextControls.LerpCharColor(m_TextAsset, randomChar, TextControls.GetCharColor(m_TextAsset, randomChar), Color.clear, 0.5f));
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Select(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        Select(false);
    }
    
    public void InitialiseRichTagging()
    {
        print(m_TextAsset.text);

        parsedString = TextControls.ParseRichTags(m_TextAsset);

        isInitialised = true;

        print("entered");
    }

    private void Update()
    {
        if(isInitialised)
        {
            for (int i = 0; i < parsedString.Length; ++i)
            {
                TextControls.ChangeCharFontSize(m_TextAsset, i, TextControls.GetCharFontSize(m_TextAsset, i)+10*Time.deltaTime);
            }
        }
    }

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

    private void OnDestruction()
    {
        TextControls textController = FindAnyObjectByType<TextControls>();

        if(textController.currentActiveWords.Contains(m_TextAsset))
            textController.currentActiveWords.Remove(m_TextAsset);

        Destroy(this);
    }
}
