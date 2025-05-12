using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConfrontButtonHandler : MonoBehaviour, IPointerEnterHandler
{
    public UnityAction hoverEnter/*, hoverExit*/;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverEnter.Invoke();
    }

    /*private void OnPointerExit(PointerEventData eventData)
    {
        hoverExit.Invoke();
    }*/
}
