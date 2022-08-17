using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ManagerClassBase<GameController>
{
    // ��� ���� ��� ������Ʈ ����Ʈ
    [Header("������Ʈ ����Ʈ")]
    [SerializeField] private List<BaseGameEntity> entity;

    private void Awake()
    {
        entity = new List<BaseGameEntity>();
    }

    // ��� ������Ʈ�� Updated()�� ȣ���մϴ�.
    private void Update()
    {
        for(int i =0; i<entity.Count; i++)
        {
            entity[i].Updated();
        }
    }

    // ������Ʈ�� �߰��մϴ�.
    public void AddEntity(BaseGameEntity baseEntity)
    {
        entity.Add(baseEntity);
    }
}
