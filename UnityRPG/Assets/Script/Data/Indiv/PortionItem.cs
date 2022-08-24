using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem
{
    public PortionItemData _PortionData { get; private set; }

    public PortionItem(PortionItemData data, int amount = 1) : base(data) { }

}
