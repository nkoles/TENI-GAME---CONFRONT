using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCheckHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonDetailHighlighting.ButtonSelected.Invoke(id);
        AudioManager.instance.PlaySFX("ButtonHover");
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX("ButtonPress");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ButtonDetailHighlighting.ButtonDeselected.Invoke();
    }
}
