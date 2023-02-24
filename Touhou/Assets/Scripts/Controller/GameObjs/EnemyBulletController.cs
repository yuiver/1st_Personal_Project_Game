using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //난이도에 따라 속도 보정값을 추가한다.
    private float bulletSpeed = 0.5f;

    private Vector2 targetPos = default;
    private Vector2 bulletPos = default;

    Vector3 bulletDir = default;
    Vector3 norDir = default;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    void Start()
    {
    }

    void OnEnable()
    {
        targetPos = PlayerController.playerPosition;
        bulletPos = gameObject.transform.localPosition;

        bulletDir = new Vector3(targetPos.x - bulletPos.x, targetPos.y - bulletPos.y, 0f);
        norDir = bulletDir.normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = norDir * bulletSpeed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);


        if (gameObject.transform.localPosition.x >= 250.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.x <= -600.0f)
        {
            OverScreen();
        }
        if (gameObject.transform.localPosition.y >= 450.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.y <= -450.0f)
        {
            OverScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
