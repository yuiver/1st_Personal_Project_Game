using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int speed = 10; //스피드 
    float xMove, yMove;

    public Animator anim;
    public Vector2 playerPosition = default;
    float coolTimeCheck = 0;

    bool isShooting = default;


    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    Transform parent_Tf = default;

    public Transform playerBulletPoolRoot { get; set; }

    void Start()
    {
        isShooting = false;

        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;
        playerPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        List<GameObject> playerBulletList = new List<GameObject>();

        playerBulletPoolRoot = new GameObject().transform;
        playerBulletPoolRoot.SetParent(parent_Tf, false);
        playerBulletPoolRoot.name = $"@{Define.PLAYER_BULLET_PREFAB_PATH}_Obj";

        for (int i = 0; i < 150; i++)
        {
            playerBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}", playerBulletPoolRoot));
        }
        foreach (GameObject playerBulletObj in playerBulletList)
        {
            Managers.Resource.Destroy(playerBulletObj);
        }

        StartCoroutine(ShootCoroutine());
    }

    void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }
    void Update()
    {

        //플레이어의 현재위치
        playerPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        xMove = 0;
        yMove = 0;

        //플레이어가 이동하는 위치
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.localPosition.x < 172.0f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            xMove = -speed * Time.deltaTime;
            if (0 > xMove) { anim.SetBool("isMoving", true); }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.localPosition.x > -550.0f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            xMove = -speed * Time.deltaTime;
            if (0 > xMove) { anim.SetBool("isMoving", true); }
        }
        if (Input.GetKey(KeyCode.UpArrow) && gameObject.transform.localPosition.y < 400.0f)
        {
            yMove = speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && gameObject.transform.localPosition.y > -400.0f)
        {
            yMove = -speed * Time.deltaTime;
        }
        this.transform.Translate(new Vector3(xMove, yMove, 0));

        if (xMove == 0)
        {
            anim.SetBool("isMoving", false);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        // Z 키를 누르면 발사 시작
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isShooting = true;
        }

        // Z 키를 떼면 발사 중지
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }

    }

        IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (isShooting == true)
                {
                    //Debug.Log("shoot");
                    GameObject go =
                    Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}",playerBulletPoolRoot);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = playerPosition;
                }

                //0.5초 대기
                yield return new WaitForSeconds(0.5f);
            }
        }



}