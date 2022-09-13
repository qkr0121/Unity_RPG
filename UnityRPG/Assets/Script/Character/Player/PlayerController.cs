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

    private void Start()
    {
        _PlayerCharacter = GetComponentInChildren<Player>();
        _ClickParticle = (Instantiate(Resources.Load("Particle/MovePoint"), transform) as GameObject).GetComponent<ParticleSystem>();
        _PlayerCharacter.Setup();
    }

    private void Update()
    {
        // ȸ�� (���� ����Ʈ)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Dodge]);
        }
        // �̵� (���콺 ������)
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Ŭ����ƼŬ�� ����մϴ�.
            _ClickParticle.transform.position = InputManager.Instance.mousePos;
            _ClickParticle.Play();

            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        else if(Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
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
        // ��ų(QWE)
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // ����� ��ų�� ���ų� ��Ÿ���̸� ��ų�� ������� �ʽ��ϴ�.
            if (_PlayerCharacter.characterInfo.skills[(int)SkillType.Q] == null ||
                !_PlayerCharacter.characterInfo.skills[(int)SkillType.Q].skillInfo.useable) return;

            _PlayerCharacter.skillType = SkillType.Q;
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        }
        // �κ��丮Ȱ��ȭ/��Ȱ��ȭ
        else if(Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.inventoryUI.activeSelf)
                UIManager.Instance.inventoryUI.SetActive(false);
            else
                UIManager.Instance.inventoryUI.SetActive(true);
        }
    }
}
