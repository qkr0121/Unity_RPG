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

    private int beginSiblingIndex;

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

        _Gr.Raycast(pointED, rcResult);

        // �� canvas �̸� �������� �ʽ��ϴ�.
        if (rcResult.Count <= 1) return null;
        
        return rcResult[1].gameObject.GetComponent<T>();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /// MovalbeItem �� Ŭ�������� UI�� �߾Ӱ� Ŭ���� InventorySlot �� �����մϴ�.
            beginInventorySlot = GetComponentInRaycast<InventorySlotUI>();

            if (beginInventorySlot == null || !beginInventorySlot.hasItem) return;

            itemIconUI = beginInventorySlot.item;

            mouseToUICenter = pointED.position - new Vector2(itemIconUI.position.x, itemIconUI.position.y);

            beginSiblingIndex = beginInventorySlot.transform.GetSiblingIndex();
            beginInventorySlot.transform.SetAsLastSibling();
            
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
            if (itemIconUI != null)
            {
                // UI ������ �����մϴ�.
                beginInventorySlot.transform.SetSiblingIndex(beginSiblingIndex);

                finishInventorySlot = GetComponentInRaycast<InventorySlotUI>();

                if (finishInventorySlot == null) finishInventorySlot = beginInventorySlot;

                // ������ ��ġ�� �����մϴ�.
                itemIconUI.position = finishInventorySlot.transform.position;

                Debug.Log(beginInventorySlot.item);
                Debug.Log(finishInventorySlot.item);
                // �������� ��ȯ�մϴ�.
                beginInventorySlot.SwapItem(finishInventorySlot);
            }

            // �ʱ�ȭ
            itemIconUI = null;
        }
    }
}
