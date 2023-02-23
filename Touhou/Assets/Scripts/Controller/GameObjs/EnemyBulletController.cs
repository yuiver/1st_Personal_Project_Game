using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //난이도에 따라 속도 보정값을 추가한다.
    private float bulletSpeed = 0.5f;
    private Vector2 targetPos = default;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = PlayerController.playerPosition;
    }

    void OnEnable()
    {
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //Vector2 direction = new Vector2(0f, 1f);
        //rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
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
