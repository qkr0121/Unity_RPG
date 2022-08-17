using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private GraphicRaycaster _Gr;

    private List<RaycastResult> rcResult;

    private PointerEventData pointED;

    private void Start()
    {
        _Gr = UIManager.Instance.canvas.GetComponent<GraphicRaycaster>();
        rcResult = new List<RaycastResult>();
        pointED = new PointerEventData(null);
    }

    private void Update()
    {
        pointED.position = Input.mousePosition;
        OnPointerDown();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _Gr.Raycast(pointED, rcResult);

            RectTransform rt = rcResult[0].gameObject.GetComponent<RectTransform>();

            Debug.Log(rt);
        }
    }
}
