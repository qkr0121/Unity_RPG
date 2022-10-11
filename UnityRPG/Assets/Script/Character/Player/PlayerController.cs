using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class PlayerController : CharacterController
{
    // ��Ʈ���� �÷��̾�ĳ����
    private Player _PlayerCharacter;

    // Ŭ����ƼŬ �ý���
    private ParticleSystem _ClickParticle;

    private SkillBarUI skillBarUI;

    // Key Dictionary
    private Dictionary<KeyCode, Action> keyDictionary;

    private void Start()
    {
        _PlayerCharacter = GetComponentInChildren<Player>();
        _ClickParticle = (Instantiate(Resources.Load("Particle/MovePoint"), transform) as GameObject).GetComponent<ParticleSystem>();
        _PlayerCharacter.Setup();
        skillBarUI = UIManager.Instance.skillBarUI;

        keyDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.LeftShift, KeyDown_LeftShift },
            { KeyCode.Q,         KeyDown_QWE },
            { KeyCode.W,         KeyDown_QWE },
            { KeyCode.E,         KeyDown_QWE }, 
            { KeyCode.R,         KeyDown_R },
            { KeyCode.I,         KeyDown_I }
        };


    }

    private void Update()
    {
        // ���콺 �Է�
        // �̵� (���콺 ������)
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Ŭ����ƼŬ�� ����մϴ�.
            _ClickParticle.transform.position = InputManager.Instance.mousePos;
            _ClickParticle.Play();

            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        else if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        // ���� (���콺 ����)
        else if (Input.GetMouseButtonDown(0))
        {
            // ����� ��ų�� ���ų� ��Ÿ���̸� ��ų�� ������� �ʽ��ϴ�.
            if (_PlayerCharacter.characterInfo.skills[(int)SkillType.A] == null ||
                !_PlayerCharacter.characterInfo.skills[(int)SkillType.A].skillInfo.useable) return;

            _PlayerCharacter.skillType = SkillType.A;
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        }

        // Key �Է�
        if (Input.anyKeyDown)
        {
            foreach(var dic in keyDictionary)
            {
                if(Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    private void KeyDown_LeftShift()
    {
        _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Dodge]);
    }

    private void KeyDown_QWE()
    {
        SkillType skillType = new SkillType();

        if (Input.GetKeyDown(KeyCode.Q))
            skillType = SkillType.Q;
        else if (Input.GetKeyDown(KeyCode.W))
            skillType = SkillType.W;
        else if (Input.GetKeyDown(KeyCode.E))
            skillType = SkillType.E;

        // ����� ��ų�� ���ų� ��Ÿ���̸� ��ų�� ������� �ʽ��ϴ�.
        if (_PlayerCharacter.characterInfo.skills[(int)skillType] == null ||
            !_PlayerCharacter.characterInfo.skills[(int)skillType].skillInfo.useable) return;

        _PlayerCharacter.skillType = skillType;
        _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        skillBarUI.CoolDown((int)skillType);       
    }

    private void KeyDown_R()
    {
        // ����� ��ų�� ���ų� ��Ÿ���̸� ��ų�� ������� �ʽ��ϴ�.
        if (_PlayerCharacter.characterInfo.skills[(int)SkillType.R] == null ||
            !_PlayerCharacter.characterInfo.skills[(int)SkillType.R].skillInfo.useable) return;

        // �÷��̾��� ü���� �ִ�ü���̸� ������� �ʽ��ϴ�.
        if (_PlayerCharacter.characterInfo.health == 100) return;

        _PlayerCharacter.skillType = SkillType.R;
        _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        skillBarUI.CoolDown((int)SkillType.R);
    }

    private void KeyDown_I()
    {
        if (UIManager.Instance.inventoryUI.activeSelf)
            UIManager.Instance.inventoryUI.SetActive(false);
        else
            UIManager.Instance.inventoryUI.SetActive(true);
    }

}
