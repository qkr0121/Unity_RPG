using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAAtack : Skill
{
    private void Start()
    {
        skillInfo = new SkillInfo(
            AttackType.Melee,
            "aAttack",
            3.0f,
            1,
            1f,
            true);

        _AttackRange = GetComponent<BoxCollider>();
    }

    public override void SkillEffect()
    {
        // �ִϸ��̼� ���
        _Character.animator.SetTrigger(skillInfo.skillName);

        runningCoroutine = StartCoroutine(SetCollider());
    }

    protected override IEnumerator SetCollider()
    {
        yield return new WaitForSeconds(0.3f);
        // ���� ������ ��Ÿ���� collider �� Ȱ��ȭ�մϴ�.
        _AttackRange.enabled = true;
    }

    public override void SkillFinish()
    {
        StopCoroutine(runningCoroutine);
        _AttackRange.enabled = false;
    }

}
