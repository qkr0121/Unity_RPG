using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ����(�÷��̾�, ����) �� ������ ����
namespace CharacterState
{
    public class Idle : State<Character>
    {
        public override void Enter(Character entity)
        {
            entity.animator.SetBool("idle", true);
        }

        public override void Execute(Character entity)
        {
        }

        public override void Exit(Character entity)
        {
            entity.animator.SetBool("idle", false);
        }
    }

    public class Move : State<Character>
    {
        private Vector3 destination;

        public override void Enter(Character entity)
        {
            // ���������� �����մϴ�
            destination = entity.desirePos;

            entity.animator.SetBool("move", true);

            // �������� �̵��մϴ�
            entity.agent.SetDestination(destination);

        }

        public override void Execute(Character entity)
        {
            // �̵��� �������� Idle ���·� ���ư��ϴ�.
            if (entity.agent.remainingDistance <= 0.1f)
            {
                entity.stateMachine.ChangeState(entity.characterState[(int)Character.State.Idle]);
            }
            else
            {
                // ĳ���Ͱ� �̵������� �ٶ󺸵����մϴ�.
                Vector3 dir = (new Vector3(entity.agent.steeringTarget.x, entity.transform.position.y, entity.agent.steeringTarget.z)
                    - entity.transform.position).normalized;

                if (dir != Vector3.zero)
                    entity.characterObject.transform.forward = dir;
                else
                {
                    dir = (entity.desirePos - entity.transform.position).normalized;
                    dir.y = 0;
                    entity.characterObject.transform.forward = dir;
                }
            }
        }

        public override void Exit(Character entity)
        {
            // �̵��� �����մϴ�.
            entity.agent.ResetPath();
            entity.animator.SetBool("move", false);
        }
    }

    public class Attack : State<Character>
    {
        private float leftTime;
        private SkillType skillType;
        public override void Enter(Character entity)
        {
            // ��ų ���� �ٸ� ����(��ų), �̵��� �� �� �����ϴ�.
            if (entity.skillType != SkillType.A)
            {
                entity.characterState[(int)Character.State.Attack].tryChangeState = false; 
            }
            entity.characterState[(int)Character.State.Move].tryChangeState = false;
            entity.characterState[(int)Character.State.Idle].tryChangeState = false;

            skillType = entity.skillType;

            leftTime = 0;

            // �����ϴ� ������ �ٶ󺸵����մϴ�.
            Vector3 dir = (entity.desirePos - entity.transform.position).normalized;
            dir.y = 0;
            entity.characterObject.transform.forward = dir;

            // ������ �մϴ�.
            entity.characterInfo.skills[(int)skillType].SkillStart();
        }

        public override void Execute(Character entity)
        {
            leftTime += Time.deltaTime;
            if (leftTime >= entity.characterInfo.skills[(int)entity.skillType].skillInfo.actTime)
            {
                entity.characterState[(int)Character.State.Idle].tryChangeState = true;
                entity.stateMachine.ChangeState(entity.characterState[(int)Character.State.Idle]);
            }
        }

        public override void Exit(Character entity)
        {
            entity.characterInfo.skills[(int)skillType].SkillFinish();
            entity.characterState[(int)Character.State.Idle].tryChangeState = true;
            entity.characterState[(int)Character.State.Move].tryChangeState = true;
            entity.characterState[(int)Character.State.Attack].tryChangeState = true;
        }
    }

    public class Hit : State<Character>
    {
        private float leftTime;
        private Renderer renderer;
        public override void Enter(Character entity)
        {
            entity.characterState[(int)Character.State.Idle].tryChangeState = false;
            entity.characterState[(int)Character.State.Move].tryChangeState = false;
            entity.characterState[(int)Character.State.Attack].tryChangeState = false;

            entity.animator.SetTrigger("hit");

            leftTime = 1f;

            renderer = entity.characterObject.GetComponentInChildren<Renderer>();
            renderer.material.color = Color.red;
        }

        public override void Execute(Character entity)
        {
            leftTime -= Time.deltaTime;

            if (leftTime <= 0)
            {
                entity.characterState[(int)Character.State.Idle].tryChangeState = true;
                entity.stateMachine.ChangeState(entity.characterState[(int)Character.State.Idle]);
            }
        }

        public override void Exit(Character entity)
        {
            renderer.material.color = Color.white;
            entity.characterState[(int)Character.State.Move].tryChangeState = true;
            entity.characterState[(int)Character.State.Attack].tryChangeState = true;
        }
    }

    public class Die : State<Character>
    {
        private float leftTime;
        MonsterController monsterController;
        public override void Enter(Character entity)
        {
            monsterController = entity.GetComponentInParent<MonsterController>();

            entity.characterState[(int)Character.State.Idle].tryChangeState = false;
            entity.characterState[(int)Character.State.Move].tryChangeState = false;
            entity.characterState[(int)Character.State.Attack].tryChangeState = false;
            entity.characterState[(int)Character.State.Hit].tryChangeState = false;

            entity.animator.SetTrigger("die");

            leftTime = 2;

        }

        public override void Execute(Character entity)
        {
            leftTime -= Time.deltaTime;

            if (leftTime <= 0)
            {
                entity.characterState[(int)Character.State.Idle].tryChangeState = true;
                monsterController.onDieEvent!.Invoke();
            }

        }

        public override void Exit(Character entity)
        {
        }
    }

    public class Dodge : State<Character>
    {
        private float fistSpeed;
        private float leftTime;
        public override void Enter(Character entity)
        {
            // �ʱ�ӵ� ���� �� ȸ�Ǽӵ��� �����մϴ�.
            fistSpeed = entity.characterInfo.speed;
            entity.characterInfo.speed = 10;

            entity.characterState[(int)Character.State.Move].tryChangeState = false;
            entity.characterState[(int)Character.State.Attack].tryChangeState = false;

            leftTime = 1f;
            entity.animator.SetTrigger("dodge");

            // ȸ�� ������ ���ϰ� ȸ���մϴ�.
            Vector3 dir = entity.desirePos;
            entity.agent.SetDestination(dir + (dir - entity.transform.position).normalized * 5.0f);
            entity.characterObject.transform.forward = (dir - entity.transform.position).normalized;
        }

        public override void Execute(Character entity)
        {
            leftTime -= Time.deltaTime;

            // ȸ�ǰ� ������ Idle ���·� ���ư��ϴ�.
            if (leftTime <= 0)
            {
                entity.stateMachine.ChangeState(entity.characterState[(int)Character.State.Idle]);
            }
        }

        public override void Exit(Character entity)
        {
            entity.characterState[(int)Character.State.Move].tryChangeState = true;
            entity.characterState[(int)Character.State.Attack].tryChangeState = true;
            entity.characterInfo.speed = fistSpeed;
            entity.agent.ResetPath();
        }
    }



}



