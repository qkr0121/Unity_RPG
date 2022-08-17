using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerClassBase<T> : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        // 유일성을 위해 이미 선언되었는지 확인
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<ManagerClassBase<T>>();
                if(obj != null)
                {
                    instance = obj.GetComponent<T>();
                }
                else
                {
                    var newobj = new GameObject().AddComponent<ManagerClassBase<T>>();
                    instance = newobj.GetComponent<T>();
                }
            }

            return instance;
        }
    }
}
