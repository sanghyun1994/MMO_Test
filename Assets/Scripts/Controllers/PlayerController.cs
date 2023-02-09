using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    //int mask = (1 << 8); //1�� 8������ �������� ����Ʈ �������� �о������
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    
    PlayerStat _stat;
    bool _stopAttack = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        #region Ű���� �Է�
        //Managers.Input.KeyAction -= OnKeyBoard;
        //Managers.Input.KeyAction += OnKeyBoard;
        #endregion
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent; 
        
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        if (gameObject.GetComponentInChildren<UI_MainHpbar>() == null)
            Managers.UI.MakeWorldSpaceMainHpBar<UI_MainHpbar>(transform);

    }
    
   
    protected override void UpdateMoving()
    {
        

        //���Ͱ� �� �����Ÿ����� ��������� ��� �����Ѵ�
        if (_lockTarget != null)
        {
            // magnitude = ������ ���̸� ��ȯ
            float distance = (_destPos - transform.position).magnitude;
            // �ӽ÷� �����Ÿ��� 1�� �ϵ��ڵ��صд�. ���߿� �����Ÿ��� ������ ���� ���������� ���� �ٸ��� ����� ��
            if (distance <= 1)
            {
                State = Define.State.Attack;
                return;
            }
        }

        // �̵�
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f) // �����ߴ��� ���θ� Ȯ��, �Ÿ�üũ
        {
            State = Define.State.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
                return;
            }

            float moveDest = Mathf.Clamp(_stat.MovSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDest;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

    }
    protected override void UpdateAttack()
    {
      if(_lockTarget != null)
      {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat , 20 * Time.deltaTime);

      }
    }
    void OnHitEvent()
    {

        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }

        if(_stopAttack)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Attack;
        }
    }

    #region ���� ����
    //if (_MoveToDest)
    //{
    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //    anim.Play("WAIT_RUN");
    //}
    //else
    //{
    //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
    //    Animator anim = GetComponent<Animator>();
    //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //    anim.Play("WAIT_RUN");
    //}
    #endregion

    //void OnKeyBoard()
    //{
    //    //_yAngle += Time.deltaTime * 100.0f;

    //    //���� ȸ����
    //    //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

    //    // +=delta
    //    //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

    //    //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

    //    //Quaternion.Euler();
    //    //Quaternion qt = transform.rotation;

    //    // inversedirection => ���带 ���÷�
    //    // ��ȯ�� �������� translate��

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation =
    //        Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += (Vector3.forward * Time.deltaTime * _speed);
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation =
    //        Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += (Vector3.back * Time.deltaTime * _speed);
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation =
    //        Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += (Vector3.left * Time.deltaTime * _speed);
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation =
    //        Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += (Vector3.right * Time.deltaTime * _speed);
    //    }

    //}

    void OnMouseEvent(Define.MouseEvent evt)
    {
        #region ���콺 �� �������� �̵����� �ʵ���
        //if (evt != Define.MouseEvent.Click)
        //    return;
        #endregion        
        #region ����ĳ��Ʈ �������� ����
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //Vector3 dir = mousePos - Camera.main.transform.position;
        //dir = dir.normalized;
        #endregion
        
        switch(State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Attack:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopAttack = true;
                }
                break;
                
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        
        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _stopAttack = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;

            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopAttack = true;
                break;
        }

    }

}
