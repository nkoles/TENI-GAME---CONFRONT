using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConfrontButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public ConfrontScript confront;
    public int buttonNo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        confront.hoveredButton = buttonNo;
        confront.hoverEnter.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        confront.selectedWeapon = buttonNo;
        confront.click.Invoke();
    }

    /*private void OnPointerExit(PointerEventData eventData)
    {
        hoverExit.Invoke();
    }*/
}
