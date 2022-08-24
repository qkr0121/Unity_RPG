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
        {
            CombineItem(changeSlotUI);           
        }
        // �ƴϸ� ��ȯ Ȥ�� �̵��մϴ�.
        else
        {
            // ������ slot �� �������� �����մϴ�.
            var changeItem = changeSlotUI.item;

            // �ش� �������� �������� �̵��մϴ�.
            item.transform.SetParent(changeSlotUI.transform);

            // ��ȯ�� slot �� �������� �ִٸ� �����ɴϴ�.
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

    // ���� �������� ��Ĩ�ϴ�.
    private void CombineItem(InventorySlotUI changeSlotUI)
    {
        changeSlotUI.itemCount += itemCount;

        // �������� �ִ밳���� ������ �ִ밳����ŭ�� ��Ĩ�ϴ�.
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

    // ���������� �����մϴ�.
    public void UpdateSlot()
    {
        // �������� 0���� �����մϴ�.
        if(itemCount == 0)
        {
            Destroy(item.gameObject);
        }

        item.anchoredPosition = Vector3.zero;

        countText.text = itemCount.ToString();
    }
}
