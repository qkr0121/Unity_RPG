using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������Ʈ���� ��ӹ޴� ���Ŭ����
public class BaseEntity : MonoBehaviour
{
    // ������Ʈ���� ������ȣ
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
