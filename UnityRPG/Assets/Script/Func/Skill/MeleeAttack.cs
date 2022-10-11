using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Skill
{
    private void Start()
    {
        skillInfo = new SkillInfo(
            AttackType.Melee,
            "meleeAttack",
            3.0f,
            1,
            1.5f,
            true);

        _AttackRange = GetComponent<BoxCollider>();
    }

    // ������ �����մϴ�.
    public override void SkillEffect()
    {
        // �ִϸ��̼� ���
        _Character.animator.SetTrigger(skillInfo.skillName);


        runningCoroutine = StartCoroutine(SetCollider());
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

    public override void SkillFinish()
    {
        StopCoroutine(runningCoroutine);
        _AttackRange.enabled = false;
    }

}
