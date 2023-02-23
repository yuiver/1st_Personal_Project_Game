using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Vector2 enemyPosition = default;

    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    Transform parent_Tf = default;

    private float bulletSpeed = 5.0f;
    Vector3 bulletDir;
    Vector3 norDir;
    Vector2 enemyPos = default;
    

    //public Transform enemyBulletPoolRoot { get; set; }
    //List<GameObject> enemyBulletList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;


        //enemyBulletPoolRoot = new GameObject().transform;
        //enemyBulletPoolRoot.SetParent(parent_Tf, false);
        //enemyBulletPoolRoot.name = $"@{Define.ENEMY_BULLET_PREFAB_PATH}_Obj";

        //for (int i = 0; i < 10; i++)
        //{
        //    enemyBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_BULLET_PREFAB_PATH}", enemyBulletPoolRoot));
        //}
        //foreach (GameObject enemyBulletObj in enemyBulletList)
        //{
        //    Managers.Resource.Destroy(enemyBulletObj);
        //}
    }

    void OnEnable()
    {
        enemyPos = gameObject.transform.localPosition;
        StartCoroutine(ShootCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        if (gameObject.transform.localPosition.x >= 300.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.x <= -650.0f)
        {
            OverScreen();
        }
        if (gameObject.transform.localPosition.y >= 500.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.y <= -500.0f)
        {
            OverScreen();
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            ShootBullets();
            //    //Debug.Log("shoot");
            //    GameObject go =
            //    Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_BULLET_PREFAB_PATH}", enemyBulletPoolRoot);
            //    go.transform.localScale = Vector3.one;
            //    go.transform.localPosition = enemyPosition;

            //0.5초 대기
            yield return new WaitForSeconds(3.0f);
        }
    }
    void ShootBullets()
    {
        // 중앙에 있는 탄환 발사
        bulletDir = new Vector3(enemyPos.x - PlayerController.playerPosition.x, enemyPos.y - PlayerController.playerPosition.y, 0f);
        norDir = bulletDir.normalized;
        ShootBullet(norDir);
        this.gameObject.transform.Translate(norDir * Time.deltaTime * bulletSpeed);
    }


        void ShootBullet(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_BULLET_PREFAB_PATH}", GamePlayScene.enemyBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        go.transform.localPosition = enemyPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }

}
