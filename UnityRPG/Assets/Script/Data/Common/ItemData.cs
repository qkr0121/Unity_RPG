using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 공통 아이템 데이터
public abstract class ItemData : ScriptableObject
{
    [SerializeField] private int _Id;
    [SerializeField] private string _Name;
    [SerializeField] private string _ToolTip;
    [SerializeField] private Sprite _IconSprite;
    [SerializeField] private GameObject _DropItemPrefab;

    public int id => _Id;
    public string name => _Name;
    public string tooltip => _ToolTip;
    public Sprite iconSprite => _IconSprite;

    public abstract Item CreateItem();
}
