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
    private InventorySlotUI beginInventorySlot;

    // �� InventorySlotUI
    private InventorySlotUI finishInventorySlot;

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

    private T GetComponentInRaycast<T>() where T : Component
    {
        rcResult.Clear();

        // ���콺 Ŭ���� ������ �������� itemIconUI�� �����մϴ�.
        _Gr.Raycast(pointED, rcResult);

        // �� canvas �̸� �������� �ʽ��ϴ�.
        if (rcResult.Count == 0) return null;

        return rcResult[0].gameObject.GetComponentInParent<T>();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /// MovalbeItem �� Ŭ�������� UI�� �߾Ӱ� Ŭ���� InventorySlot �� �����մϴ�.
            beginInventorySlot = GetComponentInRaycast<InventorySlotUI>();
            Debug.Log(beginInventorySlot);

            if (itemIconUI.gameObject.CompareTag("MovableItem"))
            {
                mouseToUICenter = pointED.position - new Vector2(itemIconUI.position.x, itemIconUI.position.y);

                for(int i=0;i<rcResult.Count;i++)
                {
                    if(rcResult[i].gameObject.GetComponent<InventorySlotUI>() != null)
                    {
                        beginInventorySlot = rcResult[i].gameObject.GetComponent<InventorySlotUI>();
                        beginInventorySlot.transform.SetAsLastSibling();
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
            {
                rcResult.Clear();
                finishInventorySlot = beginInventorySlot;

                _Gr.Raycast(pointED, rcResult);

                for (int i = 0; i < rcResult.Count; i++) 
                {
                    if (rcResult[i].gameObject.GetComponent<InventorySlotUI>() != null)
                    {
                        finishInventorySlot = rcResult[i].gameObject.GetComponent<InventorySlotUI>();
                        break;
                    }
                }

                itemIconUI.position = finishInventorySlot.transform.position;


            }

            // �ʱ�ȭ
            itemIconUI = null;
            rcResult.Clear();
        }
    }
}
