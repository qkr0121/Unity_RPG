using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Character
{
    [Header("기본공격과 스킬")]
    [SerializeField] private GameObject _AutoAttack;

    // 탐지 가능 상타를 나타냅니다.
    public bool isDetectable;

    /// <summary>
    /// monsterstructure로 만들것들
    /// </summary>

    // 몬스터 번호
    public int monsterNum;

    protected override void Awake()
    {
        base.Awake();

        characterInfo = new CharacterInfo(this);

        characterInfo.skills = new Skill[1];
        characterInfo.skills[(int)SkillType.A] = _AutoAttack.GetComponentInChildren<Skill>();

        _CharacterState = new State<Character>[5];
        _CharacterState[(int)State.Idle] = new CharacterState.Idle();
        _CharacterState[(int)State.Move] = new CharacterState.Move();
        _CharacterState[(int)State.Attack] = new CharacterState.Attack();
        _CharacterState[(int)State.Hit] = new CharacterState.Hit();
        _CharacterState[(int)State.Die] = new CharacterState.Die();

        _StateMachine = new StateMachine<Character>();
        _StateMachine.SetUP(this, _CharacterState[(int)State.Idle]);

        monsterNum = 0;


        base.Setup();
    }
    private void OnEnable()
    {
        characterInfo.health = 100;
    
        _CharacterState[(int)Character.State.Idle].tryChangeState = true;
        _CharacterState[(int)Character.State.Move].tryChangeState = true;
        _CharacterState[(int)Character.State.Attack].tryChangeState = true;
        _CharacterState[(int)Character.State.Hit].tryChangeState = true;
        _CharacterState[(int)Character.State.Die].tryChangeState = true;

        characterInfo.skills[(int)SkillType.A].skillInfo.useable = true;

    }

    private void Update()
    {
        // 스폰 지역에 있으면 탐지 가능상태로 변경합니다.
        Vector3 dis = transform.localPosition;
        dis.y = 0;
        if(dis.magnitude <= 0.1f)
        {
            isDetectable = true;
        }
    }

    public override void Updated()
    {
        _StateMachine.Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Skill")
        {
            // 슈퍼아머가 아니고 탐지가 가능하면 피격상태로 변환합니다.
            if (!superAmmor && isDetectable)
            {
                
                // 체력바를 UI에 표시합니다.
                UIManager.Instance.healthBarUI.AddMonster(this);

                // 데미지를 입습니다.
                characterInfo.health -= 50;

                if (characterInfo.health <= 0)
                    _StateMachine.ChangeState(_CharacterState[(int)Character.State.Die]);
                else
                    _StateMachine.ChangeState(_CharacterState[(int)Character.State.Hit]);

            }
        }
    }

}
