using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CharacterController
{
    [Header("���� ����")]
    [SerializeField] private float d_Radius;

    [Header("������ Layer")]
    [SerializeField] private LayerMask _DectectLayer;

    // ��Ʈ���� Monster
    private Monster _Monster;
    public Monster monster
    {
        get { return _Monster; }

        set { _Monster = value; }
    }

    // ������ �÷��̾�
    private Collider[] _DectectedPlayers;

    // ���� �̺�Ʈ
    public System.Action onDieEvent;

    // �ڷ�ƾ
    private Coroutine coroutine;

    public int spawnNum;

    private void Start()
    {
        onDieEvent += () =>
        {
            UIManager.Instance.healthBarUI.RemoveMonster(_Monster);

            MonsterSpawn spawner = GetComponentInParent<MonsterSpawn>();
            spawner.StartSummon(spawnNum);

            _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Idle]);

            ObjectPoolManager.Instance.Despawn(_Monster.monsterNum, _Monster.gameObject);
        };
    }

    private void Update()
    {
        // ���� ������Ʈ�� ��Ȱ��ȭ �Ǿ��ų� null �̸� �������� �ʽ��ϴ�.
        if (!_Monster.gameObject.activeSelf || _Monster == null)
        {
            return;
        }

        // �����մϴ�.
        _DectectedPlayers = Physics.OverlapSphere(_Monster.transform.position, d_Radius, _DectectLayer);

        // ���� ���� ���¿��� ������ �����ϸ�
        if (_DectectedPlayers.Length > 0 && _Monster.isDetectable)
        {
            // �÷��̾ �i���ϴ�.
            InRangeAction();
        }
    }

    private void InRangeAction()
    {
        // �÷��̾���� �����Ÿ��� ����մϴ�.
        float remainDistance = (_Monster.transform.position - _DectectedPlayers[0].transform.position).magnitude;

        // �÷��̾� ��ġ�� ������
        _Monster.desirePos = _DectectedPlayers[0].transform.position;

        // ���� �������� �����Ÿ� �̻� �־�����
        if ((_Monster.transform.localPosition).magnitude >= 5)
        {
            coroutine = StartCoroutine(ReturnToSpawn()); 
        }
        // ���ݻ�Ÿ� �ۿ� ������
        else if (remainDistance > 2.0f)
        {
            // �÷��̾ ���� �����Դϴ�.
            _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Move]);
        }
        // ���� ��Ÿ��̸�
        else
        {
            _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Idle]);

            // ����� ��ų�� �ְ� ��Ÿ���� �ƴϸ� ��ų�� ����մϴ�.
            if (_Monster.characterInfo.skills[(int)SkillType.A] != null &&
           _Monster.characterInfo.skills[(int)SkillType.A].skillInfo.useable)
            {
                // �����մϴ�.
                _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Attack]);
            }
        }

    }

    IEnumerator ReturnToSpawn()
    {
        // ������ �������� ���ư��ϴ�.
        _Monster.desirePos = transform.position;

        _Monster.characterState[(int)Character.State.Move].tryChangeState = true;
        _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Move]);


        // ���� �Ұ��� ���·� �����մϴ�.
        _Monster.isDetectable = false;

        // ü���� �ʱ�ȭ�մϴ�.
        while(true)
        {
            _Monster.characterInfo.health += 10;
            _Monster.characterInfo.health = Mathf.Clamp(_Monster.characterInfo.health, 0, 100);

            if(_Monster.characterInfo.health == 100)
            {
                // ü�¹� UI�� �����մϴ�.
                UIManager.Instance.healthBarUI.RemoveMonster(_Monster);
                
                StopCoroutine(ReturnToSpawn());

                break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
