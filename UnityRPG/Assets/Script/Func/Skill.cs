using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Melee, Range }
public enum SkillType { A, Q, W, E}

// ���ݹ��� BoxCollider �ʿ�
[RequireComponent(typeof(BoxCollider))]
// �⺻���ݰ� ��ų ���
public abstract class Skill : MonoBehaviour
{
    protected Coroutine runningCoroutine = null;

    protected Character _Character;

    protected BoxCollider _AttackRange;

    public SkillInfo skillInfo;

    private void Awake()
    {
        _Character = GetComponentInParent<Character>();
    }

    // ������ �����մϴ�.
    public void SkillStart()
    {
        skillInfo.useable = false;
        skillInfo.leftCoolTime = skillInfo.skillCool;

        SkillEffect();

        StartCoroutine(CoolDown());       
    }

    IEnumerator CoolDown()
    {

        yield return null;

        skillInfo.leftCoolTime -= Time.deltaTime;

        if (skillInfo.leftCoolTime > 0)
            StartCoroutine(CoolDown());
        else
            skillInfo.useable = true;

    }

    // ��ų ���ȿ��
    public abstract void SkillEffect();

    protected abstract IEnumerator SetCollider();


    // ������ ������ �����մϴ�.
    public abstract void SkillFinish();
}
