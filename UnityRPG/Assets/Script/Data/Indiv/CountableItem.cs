using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    public CountableItemData _CountableData { get; private set; }

    // 현재 아이템 개수
    public int amount { get; private set; }

    // 하나의 슬롯이 가질수 있는 최대개수
    public int maxAmount => _CountableData.maxAmount;

    public CountableItem(CountableItemData data, int amount = 1) : base(data)
    {
        _CountableData = data;
        SetAmount(amount);
    }

    // 개수 저장
    private void SetAmount(int Amount)
    {
        amount = Mathf.Clamp(Amount, 0, maxAmount);
    }

    // 개수 추가 및 최대 수량 초과량 반환

    // 아이템 나누기
}
