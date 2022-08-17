using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 에이전트들이 상속받는 기반클래스
public abstract class BaseGameEntity : MonoBehaviour
{
    // 상속받는 에이전트의 이름
    private string entityName;

    public virtual void Setup()
    {
        GameController.Instance.AddEntity(this);
    }

    // GameController 클래스에서 호출할 Updated
    public abstract void Updated();

}
