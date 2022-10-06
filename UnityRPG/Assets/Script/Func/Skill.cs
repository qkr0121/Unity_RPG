using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Melee, Range }
public enum SkillType { A, Q, W, E, R}

// 공격범위 BoxCollider 필요
[RequireComponent(typeof(BoxCollider))]
// 기본공격과 스킬 사용
public abstract class Skill : MonoBehaviour
{
    // 공격시 실행할 파티클
    [SerializeField] protected ParticleSystem _particleSystem;

    // 공격시 실행할 오디오
    [SerializeField] protected AudioSource _audioSource;

    protected Coroutine runningCoroutine = null;

    protected Character _Character;

    protected BoxCollider _AttackRange;

    public SkillInfo skillInfo;

    private void Awake()
    {
        _Character = GetComponentInParent<Character>();
    }

    // 공격을 시작합니다.
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

    // 스킬 사용효과
    public abstract void SkillEffect();

    protected abstract IEnumerator SetCollider();


    // 공격이 끝날때 실행합니다.
    public abstract void SkillFinish();
}
