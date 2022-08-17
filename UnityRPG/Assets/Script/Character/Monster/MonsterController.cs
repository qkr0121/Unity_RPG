using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CharacterController
{
    [Header("감지 범위")]
    [SerializeField] private float d_Radius;

    [Header("감지할 Layer")]
    [SerializeField] private LayerMask _DectectLayer;

    // 컨트롤할 Monster
    private Monster _Monster;
    public Monster monster
    {
        get { return _Monster; }

        set { _Monster = value; }
    }

    // 감지된 플레이어
    private Collider[] _DectectedPlayers;

    // 죽음 이벤트
    public System.Action onDieEvent;

    // 코루틴
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
        // 몬스터 컴포넌트가 비활성화 되었거나 null 이면 실행하지 않습니다.
        if (!_Monster.gameObject.activeSelf || _Monster == null)
        {
            return;
        }

        // 감지합니다.
        _DectectedPlayers = Physics.OverlapSphere(_Monster.transform.position, d_Radius, _DectectLayer);

        // 감지 가능 상태에서 감지에 성공하면
        if (_DectectedPlayers.Length > 0 && _Monster.isDetectable)
        {
            // 플레이어를 쫒습니다.
            InRangeAction();
        }
    }

    private void InRangeAction()
    {
        // 플레이어까지 남은거리를 계산합니다.
        float remainDistance = (_Monster.transform.position - _DectectedPlayers[0].transform.position).magnitude;

        // 플레이어 위치를 저장후
        _Monster.desirePos = _DectectedPlayers[0].transform.position;

        // 스폰 지역에서 일정거리 이상 멀어지면
        if ((_Monster.transform.localPosition).magnitude >= 5)
        {
            coroutine = StartCoroutine(ReturnToSpawn()); 
        }
        // 공격사거리 밖에 있으면
        else if (remainDistance > 2.0f)
        {
            // 플레이어를 향해 움직입니다.
            _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Move]);
        }
        // 공격 사거리이면
        else
        {
            _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Idle]);

            // 저장된 스킬이 있고 쿨타임이 아니면 스킬을 사용합니다.
            if (_Monster.characterInfo.skills[(int)SkillType.A] != null &&
           _Monster.characterInfo.skills[(int)SkillType.A].skillInfo.useable)
            {
                // 공격합니다.
                _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Attack]);
            }
        }

    }

    IEnumerator ReturnToSpawn()
    {
        // 리스폰 지역으로 돌아갑니다.
        _Monster.desirePos = transform.position;

        _Monster.characterState[(int)Character.State.Move].tryChangeState = true;
        _Monster.stateMachine.ChangeState(_Monster.characterState[(int)Character.State.Move]);


        // 감지 불가능 상태로 변경합니다.
        _Monster.isDetectable = false;

        // 체력을 초기화합니다.
        while(true)
        {
            _Monster.characterInfo.health += 10;
            _Monster.characterInfo.health = Mathf.Clamp(_Monster.characterInfo.health, 0, 100);

            if(_Monster.characterInfo.health == 100)
            {
                // 체력바 UI를 제거합니다.
                UIManager.Instance.healthBarUI.RemoveMonster(_Monster);
                
                StopCoroutine(ReturnToSpawn());

                break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
