using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private Player _Player;

    private void Start()
    {
        _Player = PlayerManager.Instance.player;
    }

}
