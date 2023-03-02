using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : EnemyBase
{
    private Vector3 bossVector3 = default;
    Rigidbody2D rb = default;
    public static float bossHp = 30000;
    public static bool bossOn = default;

    int level = LevelSelectScene.level;

    protected override void Awake()
    {
        bossOn = true;
        bossHp = 30000;
        base.Awake();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(BossMove());
        //StartCoroutine(ShootCoroutine());
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
        StopCoroutine(BossMove());
    }

    IEnumerator BossMove()
    {
        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector3.zero;
        Time.timeScale = 0;
        yield return new WaitForSeconds(1.5f);
        Managers.Sound.Play("BGM/Stage1-2",Define.Sound.Bgm);
        DelayCircleBullet(level * 9, 0.2f, level * 0.5f, Vector2.down);
        DelayCircleBullet(level * 9, 0.2f, level * 0.7f, Vector2.up);
        DelayCircleBullet(level * 9, 0.2f, level * 0.9f, Vector2.right);
        DelayCircleBullet(level * 9, 0.2f, level * 1.1f, Vector2.left);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;
        for (int i = 0; i < level; i++)
        {
            EvenNumberBullet(level * 3, 7.5f, level * 0.5f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 7.5f, level * 0.6f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            EvenNumberBullet(level * 3, 7.5f, level * 0.7f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 7.5f, level * 0.8f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            EvenNumberBullet(level * 3, 7.5f, level * 0.9f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 7.5f, level * 1.0f, Vector2.down);
            yield return new WaitForSeconds(1.0f);
        }
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2.0f);
        CircleBullet(level * 10, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 9, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 8, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 7, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 6, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 5, 1f * level, Vector2.down);

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            yield return new WaitForSeconds(1.5f);
            OddNumberBullet(level * 4, 40f / level , 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            EvenNumberBullet(level * 4, 40f / level , 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            OddNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            EvenNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
        }




        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            ShootBulletDirect(1.0f * level, new Vector2(1f-(float)(i/10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.8f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.6f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.4f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.2f * level, new Vector2(1f - (float)(i / 10f), -1));

        }


        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            yield return new WaitForSeconds(0.5f);
            DelayCircleBullet(level * 4, 0.2f, level * 0.5f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.2f, level * 0.7f, Vector2.up);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.2f, level * 0.9f, Vector2.right);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.2f, level * 1.1f, Vector2.left);
        }

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        DelayCircleBullet(level * 9, 0.2f, level * 0.5f, Vector2.down);
        DelayCircleBullet(level * 9, 0.2f, level * 0.7f, Vector2.up);
        DelayCircleBullet(level * 9, 0.2f, level * 0.9f, Vector2.right);
        DelayCircleBullet(level * 9, 0.2f, level * 1.1f, Vector2.left);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;
        for (int i = 0; i < level; i++)
        {
            EvenNumberBullet(level * 3, 5.0f, level * 0.5f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 5.0f, level * 0.6f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            EvenNumberBullet(level * 3, 5.0f, level * 0.7f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 5.0f, level * 0.8f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            EvenNumberBullet(level * 3, 5.0f, level * 0.9f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            OddNumberBullet(level * 3, 5.0f, level * 1.0f, Vector2.down);
            yield return new WaitForSeconds(1.0f);
        }
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2.0f);
        yield return new WaitForSeconds(2.0f);
        CircleBullet(level * 10, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 9, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 8, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 7, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 6, 1f * level, Vector2.down);
        yield return new WaitForSeconds(0.5f);
        CircleBullet(level * 5, 1f * level, Vector2.down);

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            yield return new WaitForSeconds(1.5f);
            OddNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            EvenNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            OddNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            EvenNumberBullet(level * 4, 40f / level, 0.5f * level, Vector2.down);
        }




        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.5f);
            ShootBulletDirect(1.0f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.8f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.6f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.4f * level, new Vector2(1f - (float)(i / 10f), -1));
            yield return new WaitForSeconds(0.2f);
            ShootBulletDirect(0.2f * level, new Vector2(1f - (float)(i / 10f), -1));

        }


        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        for (int i = 0; i < level; i++)
        {
            yield return new WaitForSeconds(0.5f);
            DelayCircleBullet(level * 4, 0.1f, level * 0.5f, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.1f, level * 0.7f, Vector2.up);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.1f, level * 0.9f, Vector2.right);
            yield return new WaitForSeconds(0.2f);
            CircleBullet(level * 12, 0.5f * level, Vector2.down);
            yield return new WaitForSeconds(0.2f);
            DelayCircleBullet(level * 4, 0.1f, level * 1.1f, Vector2.left);
        }

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.right * 1.5f;
        yield return new WaitForSeconds(2.0f);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.left * 1.5f;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerBullet"))
        {
            GetHitBulletBoss();
        }
    }

    //플레이어의 총알에 피격당했을때 생기는일
    private void GetHitBulletBoss()
    {
        PlayerController.score += 111 * LevelSelectScene.level;
        bossHp = bossHp - (float)(5 + PlayerController.power / 10);
        if (bossHp <= 0)
        {
            Managers.Sound.Play("SE/se_enep00");
            Managers.Resource.Destroy(gameObject);
        }
    }


}
