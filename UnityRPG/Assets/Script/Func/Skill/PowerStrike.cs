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

        // 파티클 재생
        _particleSystem.Play();
        // 오디오 재생
        _audioSource.Play();
        // 공격 범위를 나타내는 collider 를 활성화합니다.
        _AttackRange.enabled = true;
    }
}
