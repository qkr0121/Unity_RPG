using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    // 인벤토리 로드 하기 전 임시
    [Header("Item")]
    [SerializeField] private RectTransform Item;


    // 소유하고 있는 아이템을 나타냅니다.
    public RectTransform item { get; set; }

    // 아이템을 소유하고 있는지를 나타냅니다.
    public bool hasItem => item != null;
    
    private void Start()
    {
        item = Item;
    }

    // 다른 slot 과 아이템을 교환합니다.
    public void SwapItem(InventorySlotUI changeSlotUI)
    {
        // 자기자신이면 교환하지 않습니다.
        if (this == changeSlotUI) return;

        // 변경할 slot 의 아이템을 저장합니다.
        var changeItem = changeSlotUI.item;

        item.transform.SetParent(changeSlotUI.transform);
        changeSlotUI.item = item;

        // 교환할 slot 에 아이템이 있다면 가져옵니다.
        if(changeItem)
        {
            changeSlotUI.item.transform.SetParent(this.transform);
            item = changeItem;
        }

    }
}
