using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquareHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private Color dufaultColor = Color.white;
    private Color currentColor;
    private Color ColorWhenSelect = Color.red;

    internal delegate void SquareEventHandler(GameObject gameObject);
    internal event SquareEventHandler OnClick;
    internal event SquareEventHandler OnEnter;

    public SquareHandler()
    {
        currentColor = dufaultColor;
    }

    /// <summary>
    /// Срабатывает при клике мышкой по квадрату
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter.Invoke(this.gameObject);
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (currentColor != dufaultColor)
    //    {
    //        GetComponent<SpriteRenderer>().color = currentColor = dufaultColor;

    //        Notify.Invoke(this.gameObject);
    //    }
    //}



}
