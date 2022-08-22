using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    // �κ��丮 �ε� �ϱ� �� �ӽ�
    [Header("Item")]
    [SerializeField] private RectTransform Item;


    // �����ϰ� �ִ� �������� ��Ÿ���ϴ�.
    public RectTransform item { get; set; }

    // �������� �����ϰ� �ִ����� ��Ÿ���ϴ�.
    public bool hasItem => item != null;
    
    private void Start()
    {
        item = Item;
    }

    // �ٸ� slot �� �������� ��ȯ�մϴ�.
    public void SwapItem(InventorySlotUI changeSlotUI)
    {
        // �ڱ��ڽ��̸� ��ȯ���� �ʽ��ϴ�.
        if (this == changeSlotUI) return;

        // ������ slot �� �������� �����մϴ�.
        var changeItem = changeSlotUI.item;

        item.transform.SetParent(changeSlotUI.transform);
        changeSlotUI.item = item;

        // ��ȯ�� slot �� �������� �ִٸ� �����ɴϴ�.
        if(changeItem)
        {
            changeSlotUI.item.transform.SetParent(this.transform);
            item = changeItem;
        }

    }
}
