using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItemData : ItemData
{
    // �� �����ܿ� ������ �ִ� �ִ밳��
    [SerializeField] private int _MaxAmount;
    public int maxAmount => maxAmount;
}
