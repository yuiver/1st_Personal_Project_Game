using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    int death = default;

    [SerializeField]
    private float speed = 8; //스피드 
    private float xMove, yMove;
    private float bulletSpeed = 12.0f;
    private int bulletCountPerSide = default; // 한 쪽에 발사할 탄환 수
    private int guidedBulletCountPerSide = default; // 한 쪽에 발사할 탄환 수
    private float angleBetweenBullets = 10f; // 탄환 간의 각도 차이

    private GameObject playerCharaA = default;
    private GameObject playerCharaB = default;
    private GameObject bulletPrefab = default;

    public Animator anim;
    public static Vector2 playerPosition = default;

    private bool isShooting = default;
    private bool isOtherChara = default;

    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    Transform parent_Tf = default;

    public Transform playerBulletPoolRoot { get; set; }
    public Transform playerGuidedBulletPoolRoot { get; set; }


    void Start()
    {

        death = 0;

        playerCharaA = gameObject.FindChildObj("");
        playerCharaB = gameObject.FindChildObj("");

        //Z키를 누르면 총알이 발사되게 하는 Bool
        isShooting = false;
        //Shift를 누르면 캐릭터의 총알이 변경되게 하는 Bool
        isOtherChara = false;

        //총알을 풀링하는 코드
        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;
        playerPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        List<GameObject> playerBulletList = new List<GameObject>();

        playerBulletPoolRoot = new GameObject().transform;
        playerBulletPoolRoot.SetParent(parent_Tf, false);
        playerBulletPoolRoot.name = $"@{Define.PLAYER_BULLET_PREFAB_PATH}_Obj";

        List<GameObject> playerGuidedBulletList = new List<GameObject>();

        playerGuidedBulletPoolRoot = new GameObject().transform;
        playerGuidedBulletPoolRoot.SetParent(parent_Tf, false);
        playerGuidedBulletPoolRoot.name = $"@{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}_Obj";

        for (int i = 0; i < 35; i++)
        {
            playerBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}", playerBulletPoolRoot));
        }
        foreach (GameObject playerBulletObj in playerBulletList)
        {
            Managers.Resource.Destroy(playerBulletObj);
        }

        for (int i = 0; i < 35; i++)
        {
            playerGuidedBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}", playerGuidedBulletPoolRoot));
        }
        foreach (GameObject playerGuidedBulletObj in playerGuidedBulletList)
        {
            Managers.Resource.Destroy(playerGuidedBulletObj);
        }

    }

    void OnEnable()
    {
        //오브젝트가 활성화중이면 총알을 발사하는 코루틴을 켠다. 체크는 z키로 쏠지말지 정한다.
            StartCoroutine(ShootCoroutine());
    }
    void Update()
    {
        #region 플레이어의 파워를 체크해서 몇발의 탄환을 발사할지 결정하는 if
        if (UIController.power < 45)
        {
            bulletCountPerSide = 0;
        }
        else
        {
            bulletCountPerSide = 1;
        }
        //서브 탄환 
        if (UIController.power < 25)
        {
            guidedBulletCountPerSide = 0;
        }
        else if (UIController.power < 128)
        {
            guidedBulletCountPerSide = 1;
        }
        else
        {
            guidedBulletCountPerSide = 2;
        }
        #endregion

        #region 십자키를 눌러서 이동하는 방식
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
        #endregion 


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

        //X 키를 누르면 폭탄 발사
        if (Input.GetKeyDown(KeyCode.X))
        {
            //if(UIController.)
        }

        // 왼쪽 Shift키를 누르면 이동속도가 느려지고 캐릭터를 전환하며 피격 콜라이더를 보이게 만든다.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 0.5f;
            
        }
        // 왼쪽 Shift키를 떼면 중지
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed * 2f;
        }

    }

    private void Deathcheck()
    {
        death++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("EnemyBullet"))
        { }
    }

    IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (isShooting == true)
                {
                ShootBullets();
                ShootGuidedBullets();
                }

                //0.5초 대기
                yield return new WaitForSeconds(0.1f);
            }
        }
    #region 5way 이런식으로 규칙적 탄막을 형성가능
    void ShootBullets()
    {
        // 중앙에 있는 탄환 발사
        ShootBullet(Vector2.up);

        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    void ShootGuidedBullets()
    {

        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= guidedBulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets + (angleBetweenBullets * bulletCountPerSide);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootGuidedBullet(direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= guidedBulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets + (angleBetweenBullets * bulletCountPerSide);
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootGuidedBullet(direction);
        }
    }
    #endregion

    //벨로시티로 발사하는 탄환
    void ShootBullet(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}", playerBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f,0f,90f);
        go.transform.localPosition = playerPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
    void ShootGuidedBullet(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}", playerGuidedBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        go.transform.localPosition = playerPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }



}