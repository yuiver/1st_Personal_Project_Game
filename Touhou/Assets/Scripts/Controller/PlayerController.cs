using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    { 
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
    
    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //애니메이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

    }

    void Update()
    {
        {
            switch (_state)
            {
                case PlayerState.Die:
                    UpdateDie();
                    break;
                case PlayerState.Moving:
                    UpdateMoving();
                    break;
                case PlayerState.Idle:
                    UpdateIdle();
                    break;

            }
        }


    }

    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }
    //}

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        //if (evt != Define.MouseEvent.Click)
            //return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //마우스의 위치로 메인카메라의 위치부터 마우스가 있는 스크린의 위치로 광선을 쏜다는 뜻

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);
        //레이캐스트를 그려서 디버그로 본다 ([메인카메라 위치부터],[직선의 빛 * 거리],[광선의 색],[시간]


        //레이캐스트에 맞은 무언가로 부터 정보를 얻어오는 변수
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        //물리옵션.레이캐스트([어떤광선인지],[광선에 맞은 사물정보],[최대거리],[어떤 레이어의 정보만 호출할지])
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            //디버그로그를 호출한다 [Raycast Camera @ [충돌감지와 레이캐스트가 충돌한 오브젝트의 이름을 가져온다.]]

            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");
            //디버그로그를 호출한다 [Raycast Camera @ [광선.충돌.오브젝트.태그(충돌감지와 레이캐스트가 충돌한 오브젝트의 태그를 가져온다.)
        }
    }
}
