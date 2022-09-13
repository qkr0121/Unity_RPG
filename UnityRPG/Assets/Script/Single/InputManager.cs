using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : ManagerClassBase<InputManager>
{  
    [Header("마우스 위치 무시할 레이어")]
    [SerializeField] private LayerMask _IgnoreLayer;

    // 마우스 위치
    private Vector3 _MousePos;
    public Vector3 mousePos => _MousePos;

    // 메인 카메라
    private Camera _Camera;

    private void Awake()
    {       
        _Camera = Camera.main;
    }

    private void Update()
    {
        // 마우스의 위치를 확인합니다.
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, ~_IgnoreLayer))
        {
            _MousePos = hit.point;
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                
            }    
        }

    }
}
