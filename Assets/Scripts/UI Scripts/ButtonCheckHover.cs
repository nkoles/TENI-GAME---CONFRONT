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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonDetailHighlighting.ButtonDeselected.Invoke();
    }
}
