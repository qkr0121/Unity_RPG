using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Character
{
    [Header("�⺻���ݰ� ��ų")]
    [SerializeField] private GameObject _AutoAttack;

    // Ž�� ���� ��Ÿ�� ��Ÿ���ϴ�.
    public bool isDetectable;

    /// <summary>
    /// monsterstructure�� ����͵�
    /// </summary>

    // ���� ��ȣ
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
        // ���� ������ ������ Ž�� ���ɻ��·� �����մϴ�.
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
            // ���۾ƸӰ� �ƴϰ� Ž���� �����ϸ� �ǰݻ��·� ��ȯ�մϴ�.
            if (!superAmmor && isDetectable)
            {
                
                // ü�¹ٸ� UI�� ǥ���մϴ�.
                UIManager.Instance.healthBarUI.AddMonster(this);

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
