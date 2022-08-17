using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MovableItemUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    GraphicRaycaster ray;
    // �����̰� �� UI
    private Image _MovableUI;

    // CountableItem �� Text
    private TextMeshProUGUI countText;

    // UI �߾ӿ������� ���콺 Ŭ�������� ����
    private Vector2 mouseToUICenter;

    public int itemCode;

    public int count;

    private void Start()
    {
        _MovableUI = GetComponent<Image>();

        _MovableUI.sprite = Resources.Load<Sprite>("Sprite/Item/" + itemCode) as Sprite;

        countText = GetComponentInChildren<TextMeshProUGUI>();

        countText.text = count.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseToUICenter = eventData.position - new Vector2(_MovableUI.rectTransform.position.x, _MovableUI.rectTransform.position.y);
    }

    public void OnDrag(PointerEventData eventData)
    {

        _MovableUI.rectTransform.SetAsLastSibling();
        _MovableUI.rectTransform.position = eventData.position - mouseToUICenter;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}