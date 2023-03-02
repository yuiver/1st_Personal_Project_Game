using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyController : EnemyBase
{
    public float speed = 2.0f;



    
    protected override void Awake()
    {
        base.Awake();
        enemyHp = 1;
        enemySpeed = 0.5f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        enemyHp = 1;
        StartCoroutine(ShootCoroutine());
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (gameObject.transform.localPosition.x >= Define.maxDistX)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.x <= Define.minDistX)
        {
            OverScreen();
        }
        if (gameObject.transform.localPosition.y >= Define.maxDistY)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.y <= Define.minDistY)
        {
            OverScreen();
        }
    }
    
    IEnumerator ShootCoroutine()
    {
        float randomValue = Random.Range(0.3f, 1.0f);
        GameObject Enemy = this.gameObject;
        Rigidbody2D rb = Enemy.GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(randomValue);
        OddNumberBullet(LevelSelectScene.level + 1, 10.0f , 1.5f * (LevelSelectScene.level + 1) , Vector2.down);
        Util.Log(LevelSelectScene.level);
        //EvenNumberBullet(4, 10.0f, 1.5f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 2.0f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 2.5f, Vector2.down);
        //EvenNumberBullet(4, 10.0f, 3.0f, Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerBullet"))
        {
            GetHitBullet();
        }
    }


}
