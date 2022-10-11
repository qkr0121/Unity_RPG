using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class PlayerController : CharacterController
{
    // 컨트롤할 플레이어캐릭터
    private Player _PlayerCharacter;

    // 클릭파티클 시스템
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
        // 마우스 입력
        // 이동 (마우스 오른쪽)
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            // 클릭파티클을 재생합니다.
            _ClickParticle.transform.position = InputManager.Instance.mousePos;
            _ClickParticle.Play();

            _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Character.State.Move]);
        }
        else if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
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

        // Key 입력
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

        // 저장된 스킬이 없거나 쿨타임이면 스킬을 사용하지 않습니다.
        if (_PlayerCharacter.characterInfo.skills[(int)skillType] == null ||
            !_PlayerCharacter.characterInfo.skills[(int)skillType].skillInfo.useable) return;

        _PlayerCharacter.skillType = skillType;
        _PlayerCharacter.stateMachine.ChangeState(_PlayerCharacter.characterState[(int)Player.State.Attack]);
        skillBarUI.CoolDown((int)skillType);       
    }

    private void KeyDown_R()
    {
        // 저장된 스킬이 없거나 쿨타임이면 스킬을 사용하지 않습니다.
        if (_PlayerCharacter.characterInfo.skills[(int)SkillType.R] == null ||
            !_PlayerCharacter.characterInfo.skills[(int)SkillType.R].skillInfo.useable) return;

        // 플레이어의 체력이 최대체력이면 사용하지 않습니다.
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
