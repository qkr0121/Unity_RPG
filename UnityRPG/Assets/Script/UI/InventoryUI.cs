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

    // ������ ������ UI
    private RectTransform itemIconUI;

    // ���� IventorySlotUI
    private InventorySlotUI firstIventorySlot;

    private Vector2 mouseToUICenter;

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
        OnPointerDrag();
        OnPointerUp();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���� ������ �������� itemIconUI�� �����մϴ�.
            _Gr.Raycast(pointED, rcResult);

            itemIconUI = rcResult[0].gameObject.GetComponent<RectTransform>();

            if (itemIconUI.gameObject.tag == "MovableItem")
            {
                mouseToUICenter = pointED.position - new Vector2(itemIconUI.position.x, itemIconUI.position.y);

                for(int i=0;i<rcResult.Count;i++)
                {
                    if(rcResult[i].gameObject.GetComponent<InventorySlotUI>() != null)
                    {
                        firstIventorySlot = rcResult[i].gameObject.GetComponent<InventorySlotUI>();
                        break;
                    }
                }
            }
            else itemIconUI = null;
        }
    }

    private void OnPointerDrag()
    {
        if (itemIconUI == null) return;

        if(Input.GetMouseButton(0))
        {
            itemIconUI.position = pointED.position - mouseToUICenter;
        }
    }

    private void OnPointerUp()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(itemIconUI != null)
                itemIconUI.position = firstIventorySlot.transform.position;

            rcResult.Clear();
        }
    }
}
