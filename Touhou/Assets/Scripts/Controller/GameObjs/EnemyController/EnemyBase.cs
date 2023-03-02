using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    protected GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    protected Transform parent_Tf = default;
    //Pool할 주소의 Tranform을 캐싱
    protected Transform enemyBulletPoolRoot { get; set; }
    protected Transform powerItemPoolRoot { get; set; }
    protected Transform pointItemPoolRoot { get; set; }
    //이 오브젝트의 총알의 속도를 캐싱
    protected float bulletSpeed = 5.0f;
    protected Vector3 bulletDir;
    protected Vector3 norDir;
    protected Vector2 enemyPos = default;
    protected int enemyHp = default;
    protected float enemySpeed = default;

    GameObject GameObjectPath = default;


    protected virtual void Awake()
    {
        enemyHp = 1;

        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;


        enemyBulletPoolRoot = new GameObject().transform;
        enemyBulletPoolRoot.SetParent(parent_Tf, false);
        enemyBulletPoolRoot.name = $"@{Define.ENEMY_BULLET_PREFAB_PATH}_Obj";

        pointItemPoolRoot = new GameObject().transform;
        pointItemPoolRoot.SetParent(parent_Tf, false);
        pointItemPoolRoot.name = $"@{Define.ITEM_POINT}_Obj";

        powerItemPoolRoot = new GameObject().transform;
        powerItemPoolRoot.SetParent(parent_Tf, false);
        powerItemPoolRoot.name = $"@{Define.ITEM_POWER}_Obj";
    }
    protected virtual void OnEnable()
    {
        enemyPos = gameObject.transform.localPosition;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        enemyPos = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
    }

    IEnumerator Boss()
    {
        GameObject boss = this.gameObject;
        Rigidbody2D rb = boss.GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        //rb.velocity = direction * bossSpeed;
        EvenNumberBullet(2, 10.0f, 1.0f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 1.5f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 2.0f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 2.5f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 3.0f, Vector2.down);
    }

    protected virtual void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }

    //플레이어의 총알에 피격당했을때 생기는일
    protected virtual void GetHitBullet()
    {
        PlayerController.score += 111 * LevelSelectScene.level;
        enemyHp = enemyHp - (5 + PlayerController.power / 10);
        if (enemyHp <= 0)
        {
            Managers.Sound.Play("SE/se_enep00");
            Managers.Resource.Destroy(gameObject);
            if (PlayerController.power <= 127)
            {
                GameObject power =
                Managers.Resource.Instantiate($"GameObjs/{Define.ITEM_POWER}", powerItemPoolRoot);
                power.transform.rotation = Quaternion.identity;
                power.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                power.transform.localPosition = enemyPos + new Vector2(-1f, 0f); ;
                power.transform.localScale = Vector3.one;
            }
            else
            {
                GameObject point1 =
                Managers.Resource.Instantiate($"GameObjs/{Define.ITEM_POINT}", pointItemPoolRoot);
                point1.transform.rotation = Quaternion.identity;
                point1.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                point1.transform.localPosition = enemyPos + new Vector2(1f, 0f);
                point1.transform.localScale = Vector3.one;
            }

            GameObject point =
            Managers.Resource.Instantiate($"GameObjs/{Define.ITEM_POINT}", pointItemPoolRoot);
            point.transform.rotation = Quaternion.identity;
            point.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            point.transform.localPosition = enemyPos + new Vector2(1f,0f);
            point.transform.localScale = Vector3.one;
        }
    }


    #region ShootBullet Method
    //쏠방향을 지정가능한 총알 인스턴스 함수
    protected virtual void ShootBulletDirect(float bulletSpeed, Vector2 OriginPoint)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_BULLET_PREFAB_PATH}", enemyBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        go.transform.localPosition = enemyPos;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = OriginPoint * bulletSpeed;
    }
    //한번에 원형을 그리는 탄환을 생성하는 함수 매개변수는 한번에 발사할 총알개수 , 총알의 속도
    protected virtual void CircleBullet(int bulletCount, float bulletSpeed, Vector2 OriginPoint)
    {
        for (int i = 0; i <= bulletCount; i++)
        {
            float angle = i * (360.0f / bulletCount);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
            Vector2 direction = rotation * OriginPoint;
            ShootBulletDirect(bulletSpeed, direction);

        }
    }
    //원형으로 날아가지만 탄환이 발사하는 지연시간이 있는 탄환을 발사하는 함수, 매개변수는 발사할 총알의 개수 , 지연시간, 총알의 속도, 총알을 발사할 기준각도
    protected void DelayCircleBullet(int bulletCount, float delay, float bulletSpeed, Vector2 rotationOriginPoint)
    {
        StartCoroutine(DoDelayCircleBullet(bulletCount, delay, bulletSpeed, rotationOriginPoint));
    }
     /// @brief 총알 발사에 지연을 두는 함수
     /// @param int bulletCount : 발사할 총알의 개수
     /// @param float delay : 총알에 주는 딜레이
     /// @param float bulletSpeed : 발사할 총알의 속도 
     /// @param Vector2 rotationOriginPoint : 발사할 총알의 로테이션 원점
     /// @return 코루틴을 돌리는 딜레이 시간 객체 
     /// @see DelayCircleBullet(int bulletCount, float delay, float bulletSpeed, Vector2 rotationOriginPoint)
    private IEnumerator DoDelayCircleBullet(int bulletCount, float delay, float bulletSpeed, Vector2 rotationOriginPoint)
    {
        for (int i = 0; i <= bulletCount; i++)
        {
            float angle = i * (360.0f / bulletCount);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
            Vector2 direction = rotation * rotationOriginPoint;
            ShootBulletDirect(bulletSpeed, direction);
            yield return new WaitForSeconds(delay);
        }
    }
    //홀수인 OddWayBullet 숫자만큼 중앙 탄환의 양옆에 총알이 생긴다. (양옆에 +bulletOddCount 만큼의 탄환이 생긴다, 총알간의 각도, 총알의 속도,  )
    protected virtual void OddNumberBullet(int bulletOddCount, float angleBetweenBullets,float bulletSpeed, Vector2 OriginPoint)
    {
        // 중앙에 있는 탄환 발사
        ShootBulletDirect(bulletSpeed, OriginPoint);

        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= bulletOddCount; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
            Vector2 direction = rotation * OriginPoint;
            ShootBulletDirect(bulletSpeed,direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= bulletOddCount; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.back);
            Vector2 direction = rotation * OriginPoint;
            ShootBulletDirect(bulletSpeed, direction);
        }
    }
    //짝수인 EvenWayBullet 숫자만큼 특정각도로 양옆에 총알이 생긴다. (양옆에 +bulletOddCount 만큼의 탄환이 생긴다, 총알간의 각도, 총알의 속도,  )
    protected virtual void EvenNumberBullet(int bulletEvenCount, float angleBetweenBullets, float bulletSpeed, Vector2 OriginPoint)
    {
        // 왼쪽 측면 탄환 발사
        for (int i = 0; i < bulletEvenCount; i++)
        {
            float angle = (angleBetweenBullets / 2) + (angleBetweenBullets * i);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
            Vector2 direction = rotation * OriginPoint;
            ShootBulletDirect(bulletSpeed, direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 0; i < bulletEvenCount; i++)
        {
            float angle = (angleBetweenBullets / 2)+ (angleBetweenBullets * i);
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.back);
            Vector2 direction = rotation * OriginPoint;
            ShootBulletDirect(bulletSpeed, direction);
        }
    }

    protected virtual Vector2 TakeNormalized(Vector2 myPos, Vector2 yourPos)
    {
       Vector2 normPos = yourPos - myPos ;
        return normPos.normalized;
    }




    #endregion
}
