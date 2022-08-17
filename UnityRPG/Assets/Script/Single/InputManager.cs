using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : ManagerClassBase<InputManager>
{  
    [Header("���콺 ��ġ ������ ���̾�")]
    [SerializeField] private LayerMask _IgnoreLayer;

    // ���콺 ��ġ
    private Vector3 _MousePos;
    public Vector3 mousePos => _MousePos;

    // ���� ī�޶�
    private Camera _Camera;

    private void Awake()
    {       
        _Camera = Camera.main;
    }

    private void Update()
    {
        // ���콺�� ��ġ�� Ȯ���մϴ�.
        RaycastHit hit;
        if (Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit, 100f, ~_IgnoreLayer))
        {
            _MousePos = hit.point;
        }

    }
}
