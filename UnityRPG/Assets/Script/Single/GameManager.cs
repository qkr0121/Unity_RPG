using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _GameManager = null;

    private PlayerManager _PlayerManager;
    private InputManager _InputManager;

    private void Awake()
    {
        // ���ϼ� �˻�
        if (_GameManager != null)
            Destroy(this.gameObject);
        else
        {
            _GameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        // Manager ���
        _PlayerManager = transform.GetComponentInChildren<PlayerManager>();
        _InputManager = transform.GetComponentInChildren<InputManager>();
    }

    
}
