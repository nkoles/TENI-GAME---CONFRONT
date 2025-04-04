using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupSequence : MonoBehaviour
{
    static public bool isWon;
    public bool isStart;

    public string[] endText;

    public List<TextMeshProUGUI> text;
    public List<Button> buttons = new List<Button>();

    private void Awake()
    {
        SetupObjectLists();
        StartCoroutine(Setup());
    }

    private void SetupObjectLists()
    {
        buttons = FindObjectsOfType<Button>(false).ToList<Button>();
        text = FindObjectsOfType<TextMeshProUGUI>(false).ToList<TextMeshProUGUI>();

        TextMeshProUGUI tempText = null;

        foreach(var b in buttons)
        {
            b.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out tempText);

            if(tempText!=null && text.Contains(tempText))
            {
                text.Remove(tempText);
            }

            b.gameObject.SetActive(false);
        }
    }

    private IEnumerator Setup()
    {
        if (!isStart)
        {
            string _text = "";

            if (!isWon)
            {
                _text = endText[0];
            }
            else
            {
                _text = endText[1];
            }

            text[0].text = _text;
        } else
        {
            yield return null;
        }

        yield return TypeText(text);
        yield return LerpButtonScale(buttons);

        print("done");
    }

    int textCount, buttonCount;

    public IEnumerator TypeText(List<TextMeshProUGUI> list)
    {
        foreach(var t in list)
        {
            StartCoroutine(WaitForCoroutine(TypeText(.1f, t.text, t), true));
        }

        while(textCount < list.Count)
        {
            yield return null;
        }

        textCount = 0;

        print("out");
    }

    public IEnumerator WaitForCoroutine(IEnumerator routine, bool isText)
    {
        yield return routine;

        if (isText)
        {
            textCount++;
        }
        else
        {
            buttonCount++;
        }
    }

    public IEnumerator LerpButtonScale(List<Button> buttons)
    {
        foreach(var b in buttons)
        {
            StartCoroutine(WaitForCoroutine(LerpScale(b.transform, .3f, 0f, 1f), false));
            b.gameObject.SetActive(true);
        }

        while (buttonCount < buttons.Count)
        {
            if (buttonCount >= buttons.Count)
                break;

            yield return null;
        }

        print("scaling done");

        buttonCount = 0;
    }

    public IEnumerator LerpScale(Transform t, float speed, float start, float end)
    {
        Vector3 initLocalScale = transform.localScale;

        for(float i = 0; i < 1.1; i+= Time.deltaTime/speed)
        {
            t.localScale = Vector3.Lerp(initLocalScale * start, initLocalScale * end, i);

            yield return null;
        }

        t.localScale = initLocalScale * end;
    }

    public IEnumerator TypeText(float speed, string text, TextMeshProUGUI tmproAsset)
    {
        tmproAsset.text = "";

        foreach (var c in text)
        {
            tmproAsset.text += c;

            yield return new WaitForSeconds(speed / text.Length);
        }
    }
}
