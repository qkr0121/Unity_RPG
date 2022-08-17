using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ManagerClassBase<GameController>
{
    // 제어를 위한 모든 에이전트 리스트
    [Header("에이전트 리스트")]
    [SerializeField] private List<BaseGameEntity> entity;

    private void Awake()
    {
        entity = new List<BaseGameEntity>();
    }

    // 모든 에이전트의 Updated()를 호출합니다.
    private void Update()
    {
        for(int i =0; i<entity.Count; i++)
        {
            entity[i].Updated();
        }
    }

    // 에이전트를 추가합니다.
    public void AddEntity(BaseGameEntity baseEntity)
    {
        entity.Add(baseEntity);
    }
}
