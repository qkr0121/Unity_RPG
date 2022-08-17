using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerClassBase<UIManager>
{
    private Canvas _Canvas;
    public Canvas canvas => _Canvas ??
        GameObject.Find("Canvas").GetComponent<Canvas>();

    // Canvas ÀÇ CanasScaler
    private CanvasScaler _Cs;
    public CanvasScaler cs => _Cs;

    // ScreenRatio
    public float ratio;

    // HealthBarUI ÄÄÆ÷³ÍÆ®
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
