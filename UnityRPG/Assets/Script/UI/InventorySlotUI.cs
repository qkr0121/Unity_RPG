using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    // 인벤토리 로드 하기 전 임시
    [Header("Item")]
    [SerializeField] private RectTransform Item;

    // 아이템 이미지를 나타냅니다.
    private Image itemImage => item.GetComponent<Image>();

    // 소유하고 있는 아이템을 나타냅니다.
    public RectTransform item { get; set; }

    // 아이템을 소유상태와 셀수있는지를 나타냅니다.
    public bool hasItem => item != null;
    public bool countableItem;

    /// <summary>
    /// 아이템 텍스트에 관한 내용(텍스트, 개수)
    /// </summary>
    private TextMeshProUGUI countText;
    private int itemCount;


    private void Start()
    {
        item = Item;

        if(hasItem)
        {
            countText = GetComponentInChildren<TextMeshProUGUI>();
            itemCount = int.Parse(countText.text);
        }
    }

    // 다른 slot 과 아이템을 교환합니다.
    public void SwapItem(InventorySlotUI changeSlotUI)
    {
        // 자기자신이면 교환하지 않습니다.
        if (this == changeSlotUI)
        {
            item.anchoredPosition = Vector3.zero;
            return;
        }

        // 같은 아이템이면 합칩니다.
        if (itemImage.sprite == changeSlotUI.itemImage.sprite && countableItem)
        {
            CombineItem(changeSlotUI);           
        }
        // 아니면 교환 혹은 이동합니다.
        else
        {
            // 변경할 slot 의 아이템을 저장합니다.
            var changeItem = changeSlotUI.item;

            // 해당 슬롯으로 아이템을 이동합니다.
            item.transform.SetParent(changeSlotUI.transform);

            // 교환할 slot 에 아이템이 있다면 가져옵니다.
            if (changeItem)
            {
                changeSlotUI.item.transform.SetParent(this.transform);
            }

            changeSlotUI.item = item;
            item = changeItem;
        }

        UpdateSlot();
        changeSlotUI.UpdateSlot();
    }

    // 같은 아이템을 합칩니다.
    private void CombineItem(InventorySlotUI changeSlotUI)
    {
        changeSlotUI.itemCount += itemCount;

        // 아이템이 최대개수를 넘으면 최대개수만큼만 합칩니다.
        if (changeSlotUI.itemCount > 99)
        {
            itemCount = changeSlotUI.itemCount - 99;
            changeSlotUI.itemCount = 99;
        }
        else
            itemCount = 0;

        UpdateSlot();
        changeSlotUI.UpdateSlot();
    }

    // 슬롯정보를 갱신합니다.
    public void UpdateSlot()
    {
        // 아이템이 0개면 삭제합니다.
        if(itemCount == 0)
        {
            Destroy(item.gameObject);
        }

        item.anchoredPosition = Vector3.zero;

        countText.text = itemCount.ToString();
    }
}
