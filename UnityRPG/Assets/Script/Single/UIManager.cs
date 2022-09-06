using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerClassBase<UIManager>
{
    [Header("�κ��丮")]
    [SerializeField] private GameObject _InventoryUI;
    public GameObject inventoryUI => _InventoryUI;

    private Canvas _Canvas;
    public Canvas canvas => _Canvas ??
        GameObject.Find("Canvas").GetComponent<Canvas>();

    // Canvas �� CanasScaler
    private CanvasScaler _Cs;
    public CanvasScaler cs => _Cs;

    // ScreenRatio
    public float ratio { get; private set; }

    // HealthBarUI ������Ʈ
    private HealthBarUI _HealthBarUI;
    public HealthBarUI healthBarUI => _HealthBarUI;

    private void Start()
    {
        _Cs = canvas.GetComponent<CanvasScaler>();

        float wratio = Screen.width / cs.referenceResolution.x;
        float hratio = Screen.height / cs.referenceResolution.y;

        ratio =
            wratio * (1f - cs.matchWidthOrHeight) +
            hratio * (cs.matchWidthOrHeight);

        _HealthBarUI = _Cs.GetComponentInChildren<HealthBarUI>();
    }

}
