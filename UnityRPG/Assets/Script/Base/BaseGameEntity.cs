using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������Ʈ���� ��ӹ޴� ���Ŭ����
public abstract class BaseGameEntity : MonoBehaviour
{
    // ��ӹ޴� ������Ʈ�� �̸�
    private string entityName;

    public virtual void Setup()
    {
        GameController.Instance.AddEntity(this);
    }

    // GameController Ŭ�������� ȣ���� Updated
    public abstract void Updated();

}
