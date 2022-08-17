using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerClassBase<T> : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        // ���ϼ��� ���� �̹� ����Ǿ����� Ȯ��
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
