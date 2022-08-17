using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Character : BaseGameEntity
{
    // Character �� State
    public enum State { Idle, Move, Attack, Hit, Die, Dodge }

    [Header("ĳ���� ������Ʈ")]
    [SerializeField] private GameObject _CharacterObject;
    public GameObject characterObject => _CharacterObject;

    // Character(Player,Monster) �� ��� ����
    protected State<Character>[] _CharacterState;
    public State<Character>[] characterState => _CharacterState;

    // StateMachine ������Ʈ
    protected StateMachine<Character> _StateMachine;
    public StateMachine<Character> stateMachine => _StateMachine;

    // Character �ִϸ�����
    protected Animator _Animator;
    public Animator animator => _Animator;

    // NavMeshAgent
    protected NavMeshAgent _Agent;
    public NavMeshAgent agent => _Agent;

    // ĳ���� ����
    public CharacterInfo characterInfo;

    // ��ǥ����
    public Vector3 desirePos;

    // �Է��� ����
    public SkillType skillType;

    // ���۾Ƹ� ����
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
