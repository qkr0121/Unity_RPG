using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : ManagerClassBase<PlayerManager>
{
    private Player _Player;

    public Player player => _Player = _Player ??
        GameObject.Find("Player").GetComponentInChildren<Player>();

    // 플레이어 정보를 불러옵니다.
    public void LoadPlayer(string name)
    {
        PlayerLoadInfo playerLoadInfo = JsonManager.Instance.LoadFromJson<PlayerLoadInfo>("Player/" + name);

    }

    private void Start()
    {
        /// 플레이어 정보 저장
        //PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo(2,2,2,2);
        //
        //playerLoadInfo.items = new List<InventoryItemLoad>();
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //
        //JsonManager.Instance.SaveToJson<PlayerLoadInfo>("Player/player1", playerLoadInfo);

        /// 플레이어 정보 불러오기
        //PlayerLoadInfo playerLoadInfo = JsonManager.Instance.LoadFromJson<PlayerLoadInfo>("Player/player1");
    }
}
