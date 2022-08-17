using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MovableItemUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    GraphicRaycaster ray;
    // 움직이게 할 UI
    private Image _MovableUI;

    // CountableItem 의 Text
    private TextMeshProUGUI countText;

    // UI 중앙에서부터 마우스 클릭까지의 벡터
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