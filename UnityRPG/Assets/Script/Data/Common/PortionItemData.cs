using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item_Portion", menuName = "Inventory System/Item Data/Portion",order =3)]
public class PortionItemData : CountableItemData
{
    [Header("È¸º¹·®")]
    [SerializeField] private float _Value;
    public float value => _Value;

    public override Item CreateItem()
    {
        return new PortionItem(this);
    }
}
