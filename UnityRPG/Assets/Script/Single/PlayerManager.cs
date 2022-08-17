using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : ManagerClassBase<PlayerManager>
{
    private Player _Player;

    public Player player => _Player = _Player ??
        GameObject.Find("Player").GetComponentInChildren<Player>();

    // �÷��̾� ������ �ҷ��ɴϴ�.
    public void LoadPlayer(string name)
    {
        PlayerLoadInfo playerLoadInfo = JsonManager.Instance.LoadFromJson<PlayerLoadInfo>("Player/" + name);

    }

    private void Start()
    {
        /// �÷��̾� ���� ����
        //PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo(2,2,2,2);
        //
        //playerLoadInfo.items = new List<InventoryItemLoad>();
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //playerLoadInfo.items.Add(new InventoryItemLoad(3,3,3));
        //
        //JsonManager.Instance.SaveToJson<PlayerLoadInfo>("Player/player1", playerLoadInfo);

        /// �÷��̾� ���� �ҷ�����
        //PlayerLoadInfo playerLoadInfo = JsonManager.Instance.LoadFromJson<PlayerLoadInfo>("Player/player1");
    }
}
