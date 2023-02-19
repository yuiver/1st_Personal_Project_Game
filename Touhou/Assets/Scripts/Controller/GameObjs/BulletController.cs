using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Velocity 설정
        Vector2 direction = new Vector2(0f, 1f);
        rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }

    void OnEnable()
    {
        if (UIController.power < 20)
        { }
        else if (UIController.power < 20)
        { }
        else if (UIController.power < 20)
        { }
        else
        { }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(0f, 1f);
        rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);


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

    void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }

}