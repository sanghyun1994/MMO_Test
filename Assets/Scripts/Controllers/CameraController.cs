using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField] Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField] private GameObject _player = null;



    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            if (_player.IsVaild() == false)
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    _delta += _delta.normalized * 1.03f;
                else if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    _delta -= _delta.normalized * 1.03f;

                if (_delta.magnitude < 4)
                    _delta = _delta.normalized * 4;

                if (_delta.magnitude > 12)
                    _delta = _delta.normalized * 12;

                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }


    }
    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
