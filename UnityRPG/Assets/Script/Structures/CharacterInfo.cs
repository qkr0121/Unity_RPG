using System.Collections.Generic;
using UnityEngine;

public struct CharacterInfo
{
    Character character;

    // ü�� ����
    public float health;
    public float mana;

    // �̵��ӵ�
    private float Speed;
    public float speed
    {
        get => Speed;
        set
        {
            Speed = Mathf.Max(0, value);
            character.agent.speed = Speed;
        }
    }

    // ����
    public Skill[] skills;

    // ������
    public CharacterInfo(Character entity)
    {
        character = entity;

        health = 100;
        mana = 100;

        skills = new Skill[4];

        Speed = 3;
        speed = Speed;
    }

}

// �ҷ��� �÷��̾��� ����
[System.Serializable]
public class PlayerLoadInfo
{
    public int hat;

    public int body;

    public int bag;

    public int weapon;

    public PlayerLoadInfo(int HAT, int BODY, int BAG, int WEAPON)
    {
        hat = HAT;

        body = BODY;

        bag = BAG;

        weapon = WEAPON;
    }

    public List<InventoryItemLoad> items;
}

// �÷��̾� �κ��丮 ����
[System.Serializable]
public struct InventoryItemLoad
{
    public int id, amount, slotIndex;

    public InventoryItemLoad(int ID, int AMOUNT, int SLOTINDEX)
    {
        id = ID;

        amount = AMOUNT;

        slotIndex = SLOTINDEX;
    }
}
