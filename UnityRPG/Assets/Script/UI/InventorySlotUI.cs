using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    // �κ��丮 �ε� �ϱ� �� �ӽ�
    [Header("Item")]
    [SerializeField] private RectTransform Item;

    // ������ �̹����� ��Ÿ���ϴ�.
    private Image itemImage => item.GetComponent<Image>();

    // �����ϰ� �ִ� �������� ��Ÿ���ϴ�.
    public RectTransform item { get; set; }

    // �������� �������¿� �����ִ����� ��Ÿ���ϴ�.
    public bool hasItem => item != null;
    public bool countableItem;

    /// <summary>
    /// ������ �ؽ�Ʈ�� ���� ����(�ؽ�Ʈ, ����)
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

    // �ٸ� slot �� �������� ��ȯ�մϴ�.
    public void SwapItem(InventorySlotUI changeSlotUI)
    {
        // �ڱ��ڽ��̸� ��ȯ���� �ʽ��ϴ�.
        if (this == changeSlotUI)
        {
            item.anchoredPosition = Vector3.zero;
            return;
        }

        // ���� �������̸� ��Ĩ�ϴ�.
        if (itemImage.sprite == changeSlotUI.itemImage.sprite && countableItem)
            CombineItem(changeSlotUI);

        // ������ slot �� �������� �����մϴ�.
        var changeItem = changeSlotUI.item;
        
        // �ش� �������� ������ �̵��� ��ġ�� �ʱ�ȭ�մϴ�.
        item.transform.SetParent(changeSlotUI.transform);
        item.anchoredPosition = Vector3.zero;

        // ��ȯ�� slot �� �������� �ִٸ� �����ɴϴ�.
        if(changeItem)
        {
            changeSlotUI.item.transform.SetParent(this.transform);
            changeSlotUI.item.anchoredPosition = Vector3.zero;
        }

        changeSlotUI.item = item;
        item = changeItem;

    }

    // ���� �������� ��Ĩ�ϴ�.
    private void CombineItem(InventorySlotUI changeSlotUI)
    {
        itemCount += changeSlotUI.itemCount;

        // �������� �ִ밳���� ������ �ִ밳����ŭ�� ��Ĩ�ϴ�.
        if(itemCount > 99)
        {
            changeSlotUI.itemCount = itemCount - 99;
            itemCount = 99;
        }

        UpdateSlot();
        changeSlotUI.UpdateSlot();
    }

    // ���������� �����մϴ�.
    public void UpdateSlot()
    {
        countText.text = itemCount.ToString();
    }
}
