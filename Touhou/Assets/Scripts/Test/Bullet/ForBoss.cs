using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForBoss : MonoBehaviour
{
    private float interval = 0.5f;
    private float nextTime = 0f;


    public GameObject bulletPrefab; // 탄환의 프리팹
    public Transform bulletSpawnPoint; // 탄환 발사 위치
    public float bulletSpeed; // 탄환 속도
    public int bulletCountPerSide = 2; // 한 쪽에 발사할 탄환 수
    public float angleBetweenBullets = 10f; // 탄환 간의 각도 차이
    public int bulletCountPerSide2 = 3;

    public int bulletCountPerSide3 = 16; // 한 쪽에 발사할 탄환 수
    public float angleBetweenBullets3 = 10f; // 탄환 간의 각도 차이

    int bulletCountPerSide4 = 18; // 한 쪽에 발사할 탄환 수
    float angleBetweenBullets4 = 20f; // 탄환 간의 각도 차이

    # region 첫번째는 5way 두번째는 6way 이런식으로 규칙적 탄막을 형성가능
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

    void ShootBullets2()
    {
        // 왼쪽 측면 탄환 발사
        for (int i = 0; i < bulletCountPerSide2; i++)
        {

            float angle = angleBetweenBullets * (1 + i * 2) / 2;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 0; i < bulletCountPerSide2; i++)
        {
            float angle = angleBetweenBullets * (1 + i * 2) / 2;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    #endregion

    #region 원형으로 쏘는 탄환 각도와 탄환개수를 잘 생각하면 된다. 
    void ShootBullets3()
    {
        ShootBullet(Vector2.up);
        ShootBullet(Vector2.down);
        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide3; i++)
        {
            float angle = i * angleBetweenBullets3;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide3; i++)
        {
            float angle = i * angleBetweenBullets3;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    #endregion

    #region 원형으로 쏘는 탄환 각도와 탄환개수를 잘 생각하면 된다. 
    void ShootBullets4()
    {
        // 왼쪽 측면 탄환 발사
        for (int i = 0; i <= bulletCountPerSide4; i++)
        {
            float angle = i * angleBetweenBullets4;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    void ShootBullets5()
    {
        // 왼쪽 측면 탄환 발사
        for (int i = 0; i <= bulletCountPerSide4; i++)
        {
            float angle = i * angleBetweenBullets4 + 10f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    #endregion

    //벨로시티로 발사하는 탄환
    void ShootBullet(Vector2 direction)
    {
        bulletPrefab = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bulletPrefab.transform.SetParent(bulletSpawnPoint.parent); // 캔버스를 부모로 설정
        bulletPrefab.gameObject.SetActive(true);
        bulletPrefab.transform.localScale = Vector3.one; // 스케일 초기화
        Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //코루틴을 이용해서 시간지연주기
        //StartCoroutine(ShootCoroutain());
    }

    //켜질때만 쏘는거 실험
    private void OnEnable()
    {
        //코루틴을 이용해서 시간지연주기
        StartCoroutine(ShootCoroutain());
    }

    // Update is called once per frame
    void Update()
    {
        //인터벌 타임을 설정해서 시간지연주기
        if (Time.time >= nextTime)
        {
            nextTime += interval;

            // 실행할 코드
            //ShootBullets3();
            //ShootBullets4();
        }
    }
    IEnumerator ShootCoroutain()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            //ShootBullets();
            ShootBullets4();

            yield return new WaitForSeconds(0.2f);

            //ShootBullets2();
            ShootBullets5();
        }
    }
}
