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

    // 움직일 아이템 UI
    private RectTransform itemIconUI;

    // 시작 IventorySlotUI
    private InventorySlotUI beginInventorySlot;

    // 끝 InventorySlotUI
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

        // 빈 canvas 이면 실행하지 않습니다.
        if (rcResult.Count <= 1) return null;
        
        return rcResult[1].gameObject.GetComponent<T>();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /// MovalbeItem 을 클릭했을시 UI의 중앙과 클릭한 InventorySlot 을 저장합니다.
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
                // UI 순서를 복원합니다.
                beginInventorySlot.transform.SetSiblingIndex(beginSiblingIndex);

                finishInventorySlot = GetComponentInRaycast<InventorySlotUI>();

                if (finishInventorySlot == null) finishInventorySlot = beginInventorySlot;

                // 아이템 위치를 변경합니다.
                itemIconUI.position = finishInventorySlot.transform.position;

                Debug.Log(beginInventorySlot.item);
                Debug.Log(finishInventorySlot.item);
                // 아이템을 교환합니다.
                beginInventorySlot.SwapItem(finishInventorySlot);
            }

            // 초기화
            itemIconUI = null;
        }
    }
}
