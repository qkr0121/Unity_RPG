using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 에이전트들이 상속받는 기반클래스
public class BaseEntity : MonoBehaviour
{
    // 에이전트들의 고유번호
    private static int id_Entity = 0;

    private int id;

    public int ID
    {
        get { return id; }
        set
        {
            id = value;
            id_Entity++;
        }
    }
}
