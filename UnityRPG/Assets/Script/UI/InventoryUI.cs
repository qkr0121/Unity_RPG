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

        // 마우스 클릭시 선택한 아이템을 itemIconUI에 저장합니다.
        _Gr.Raycast(pointED, rcResult);

        // 빈 canvas 이면 실행하지 않습니다.
        if (rcResult.Count == 0) return null;

        return rcResult[0].gameObject.GetComponentInParent<T>();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /// MovalbeItem 을 클릭했을시 UI의 중앙과 클릭한 InventorySlot 을 저장합니다.
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

            // 초기화
            itemIconUI = null;
            rcResult.Clear();
        }
    }
}
