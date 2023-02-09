using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    
    [SerializeField] float _scanRange = 1;      // ���� ����
    [SerializeField] float _attackRange = 2;    // ���� ���� 

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {

        GameObject player = Managers.Game.GetPlayer();

        if (player == null)
            return;

        // �÷��̾���� �Ÿ� üũ
        float distance = (player.transform.position - transform.position).magnitude;
        if(distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }    

    }

    protected override void UpdateMoving()
    {

        //�÷��̾ �� �����Ÿ����� ��������� ��� ����
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Attack;
                return;
            }
        }

        // �̵�
        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.1f) // �����ߴ��� ���θ� Ȯ��, �Ÿ�üũ
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            if (nma != null)
            {
                nma.SetDestination(_destPos);
                nma.speed = _stat.MovSpeed;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }

    }

    protected override void UpdateAttack()
    {

    }
    void OnHitEvent()
    {

        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            if(targetStat.Hp <= 0)
            {
                Managers.Game.Despawn(targetStat.gameObject);
            }

            if(targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;

                if (distance <= _attackRange)
                    State = Define.State.Attack;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }


        }
        else
        {
            State = Define.State.Idle;
        }

    }

}

