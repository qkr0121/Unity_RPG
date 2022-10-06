using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStrike : Skill
{
    private void Start()
    {
        skillInfo = new SkillInfo(
            AttackType.Melee,
            "powerStrike",
            3.0f,
            1,
            1.5f,
            true);

        _AttackRange = GetComponent<BoxCollider>();
    }

    public override void SkillEffect()
    {
        // �ִϸ��̼� ���
        _Character.animator.SetTrigger(skillInfo.skillName);
        
        runningCoroutine = StartCoroutine(SetCollider());
    }

    public override void SkillFinish()
    {
        StopCoroutine(runningCoroutine);

        _AttackRange.enabled = false;
    }

    protected override IEnumerator SetCollider()
    {
        yield return new WaitForSeconds(0.3f);

        // ��ƼŬ ���
        _particleSystem.Play();
        // ����� ���
        _audioSource.Play();
        // ���� ������ ��Ÿ���� collider �� Ȱ��ȭ�մϴ�.
        _AttackRange.enabled = true;
    }
}
