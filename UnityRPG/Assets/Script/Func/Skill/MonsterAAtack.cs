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
        // 애니메이션 재생
        _Character.animator.SetTrigger(skillInfo.skillName);

        runningCoroutine = StartCoroutine(SetCollider());
    }

    protected override IEnumerator SetCollider()
    {
        yield return new WaitForSeconds(0.3f);
        // 공격 범위를 나타내는 collider 를 활성화합니다.
        _AttackRange.enabled = true;
    }

    public override void SkillFinish()
    {
        StopCoroutine(runningCoroutine);
        _AttackRange.enabled = false;
    }

}
