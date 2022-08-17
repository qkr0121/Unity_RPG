using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// UI�� Ŭ���Ͽ� ������ �� �ֵ��� �մϴ�.
public class MovableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("�����̰� �� UI")]
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
