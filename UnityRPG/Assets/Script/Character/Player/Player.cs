using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{

    [Header("�⺻���ݰ� ��ų")]
    [SerializeField] private GameObject _AutoAttack;
    [SerializeField] private GameObject Q_Skill;

    // �÷��̾� ����
    public PlayerStats stats;

    protected override void Awake()
    {
        base.Awake();

        characterInfo = new CharacterInfo(this);

        characterInfo.skills = new Skill[2];
        characterInfo.skills[(int)SkillType.A] = _AutoAttack.GetComponentInChildren<Skill>();
        characterInfo.skills[(int)SkillType.Q] = Q_Skill.GetComponentInChildren<Skill>();

        _CharacterState = new State<Character>[6];
        _CharacterState[(int)State.Idle] = new CharacterState.Idle();
        _CharacterState[(int)State.Move] = new CharacterState.Move();
        _CharacterState[(int)State.Attack] = new CharacterState.Attack();
        _CharacterState[(int)State.Hit] = new CharacterState.Hit();
        _CharacterState[(int)State.Die] = new CharacterState.Die();
        _CharacterState[(int)State.Dodge] = new CharacterState.Dodge();

        _StateMachine = new StateMachine<Character>();
        _StateMachine.SetUP(this, _CharacterState[(int)State.Idle]);

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
        _CharacterState[(int)Character.State.Dodge].tryChangeState = true;

        characterInfo.skills[(int)SkillType.A].skillInfo.useable = true;
        characterInfo.skills[(int)SkillType.Q].skillInfo.useable = true;
    }

    private void Update()
    {
        desirePos = InputManager.Instance.mousePos;
    }

    // GameController���� ������ ����
    public override void Updated()
    {
        _StateMachine.Execute();       
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "MonsterAttack")
        {
            // ���۾ƸӰ� �ƴϸ� �ǰݻ��·� ��ȯ�մϴ�.
            if (!superAmmor)
            {
                // �������� �Խ��ϴ�.
                characterInfo.health -= 50;

                if (characterInfo.health <= 0)
                    _StateMachine.ChangeState(_CharacterState[(int)Character.State.Die]);
                else
                    _StateMachine.ChangeState(_CharacterState[(int)Character.State.Hit]);

            }
        }
    }
}
