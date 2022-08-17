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
        // 애니메이션 재생
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
        _AttackRange.enabled = true;
    }
}
