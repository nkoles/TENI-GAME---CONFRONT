using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCheckHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material matRef;
    public int id;

    private bool _wasHighlighted;

    private Coroutine _routine;


    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        _routine = StartCoroutine(ScaleButton(Vector3.one * 1.2f));

        ButtonDetailHighlighting.ButtonSelected.Invoke(id);
        AudioManager.instance.PlaySFX("ButtonHover");
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX("ButtonPress");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();

        _routine = StartCoroutine(ScaleButton(Vector3.one));

        //ButtonDetailHighlighting.ButtonDeselected.Invoke();
    }

    public IEnumerator ScaleButton(Vector3 factor, float speed = .25f)
    {   
        Vector3 initScale = transform.localScale;

        for(float i = 0; i < 1; i+= Time.fixedDeltaTime / speed /60)
        {
            if (i + Time.fixedDeltaTime / speed * 2 > 1)
            {
                transform.localScale = factor;

                break;
            }

            transform.localScale =  Vector3.Lerp(transform.localScale, factor, i);

            yield return null;
        }

        _routine = null;
    }
}
