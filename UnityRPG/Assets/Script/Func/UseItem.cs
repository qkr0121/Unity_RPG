using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Skill
{
    private void Start()
    {
        skillInfo = new SkillInfo(
            AttackType.Melee,
            "potion",
            5.0f,
            1,
            0.5f,
            true);
    }

    public override void SkillEffect()
    {
        // 체력을 채워줍니다.
        _Character.characterInfo.health = Mathf.Clamp(_Character.characterInfo.health + 50.0f, 0, 100);

    }

    public override void SkillFinish()
    {

    }

    protected override IEnumerator SetCollider()
    {
        yield return new WaitForSeconds(0.3f);

    }
}
