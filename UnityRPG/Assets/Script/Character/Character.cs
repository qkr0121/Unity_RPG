using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Character : BaseGameEntity
{
    // Character 의 State
    public enum State { Idle, Move, Attack, Hit, Die, Dodge }

    [Header("캐릭터 오브젝트")]
    [SerializeField] private GameObject _CharacterObject;
    public GameObject characterObject => _CharacterObject;

    // Character(Player,Monster) 의 모든 상태
    protected State<Character>[] _CharacterState;
    public State<Character>[] characterState => _CharacterState;

    // StateMachine 컴포넌트
    protected StateMachine<Character> _StateMachine;
    public StateMachine<Character> stateMachine => _StateMachine;

    // Character 애니메이터
    protected Animator _Animator;
    public Animator animator => _Animator;

    // NavMeshAgent
    protected NavMeshAgent _Agent;
    public NavMeshAgent agent => _Agent;

    // 캐릭터 정보
    public CharacterInfo characterInfo;

    // 목표지점
    public Vector3 desirePos;

    // 입력한 공격
    public SkillType skillType;

    // 슈퍼아머 상태
    public bool superAmmor;

    protected virtual void Awake()
    {
        _Animator = GetComponentInChildren<Animator>();
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.updateRotation = false;
    }

    public override void Updated()
    {

    }
}
