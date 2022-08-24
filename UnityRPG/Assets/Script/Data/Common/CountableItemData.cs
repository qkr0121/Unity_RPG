using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItemData : ItemData
{
    // 한 아이콘에 가질수 있는 최대개수
    [SerializeField] private int _MaxAmount;
    public int maxAmount => maxAmount;
}
