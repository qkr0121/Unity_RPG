using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// UI를 클릭하여 움직일 수 있도록 합니다.
public class MovableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("움직이게 할 UI")]
    [SerializeField] private RectTransform MovableUI;

    private Vector2 mousePosToUIcenter;

    public void OnPointerDown(PointerEventData eventData)
    {
        mousePosToUIcenter = eventData.position - new Vector2(MovableUI.position.x, MovableUI.position.y);

    }

    public void OnDrag(PointerEventData eventData)
    {
        MovableUI.position = eventData.position - mousePosToUIcenter;
    }

}
