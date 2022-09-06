using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : CharacterController
{
    // 컨트롤할 플레이어캐릭터
    private Player _PlayerCharacter;

    // 클릭파티클 시스템
    private ParticleSystem _ClickParticle;

    private void Start()
    {
        _PlayerCharacter = GetComponentInChildren<Player>();
        _ClickParticle = (Instantiate(Resources.Load("Particle/MovePoint"), transform) as GameObject).GetComponent<ParticleSystem>();
        _PlayerCharacter.Setup();
    }

    private void Update()
    {
        // 회피 (왼쪽 쉬프트)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Dodge]);
        }
        // 이동 (마우스 오른쪽)
        else if (Input.GetMouseButtonDown(1))
        {
            // 클릭파티클을 재생합니다.
            _ClickParticle.transform.position = InputManager.Instance.mousePos;
            _ClickParticle.Play();

            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        else if(Input.GetMouseButton(1))
        {
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        // 공격 (마우스 왼쪽)
        else if (Input.GetMouseButtonDown(0))
        {
            // 저장된 스킬이 없거나 쿨타임이면 스킬을 사용하지 않습니다.
            if (_PlayerCharacter.characterInfo.skills[(int)SkillType.A] == null ||
                !_PlayerCharacter.characterInfo.skills[(int)SkillType.A].skillInfo.useable) return;
            
            _PlayerCharacter.skillType = SkillType.A;
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        }
        // 스킬(QWE)
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // 저장된 스킬이 없거나 쿨타임이면 스킬을 사용하지 않습니다.
            if (_PlayerCharacter.characterInfo.skills[(int)SkillType.Q] == null ||
                !_PlayerCharacter.characterInfo.skills[(int)SkillType.Q].skillInfo.useable) return;

            _PlayerCharacter.skillType = SkillType.Q;
            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        }
        // 인벤토리활성화/비활성화
        else if(Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.inventoryUI.activeSelf)
                UIManager.Instance.inventoryUI.SetActive(false);
            else
                UIManager.Instance.inventoryUI.SetActive(true);
        }
    }
}
