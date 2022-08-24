using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    public CountableItemData _CountableData { get; private set; }

    // ���� ������ ����
    public int amount { get; private set; }

    // �ϳ��� ������ ������ �ִ� �ִ밳��
    public int maxAmount => _CountableData.maxAmount;

    public CountableItem(CountableItemData data, int amount = 1) : base(data)
    {
        _CountableData = data;
        SetAmount(amount);
    }

    // ���� ����
    private void SetAmount(int Amount)
    {
        amount = Mathf.Clamp(Amount, 0, maxAmount);
    }

    // ���� �߰� �� �ִ� ���� �ʰ��� ��ȯ

    // ������ ������
}
