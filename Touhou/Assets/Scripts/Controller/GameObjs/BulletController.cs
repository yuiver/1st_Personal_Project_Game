using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    void Start()
    {

    }

    void OnEnable()
    {
    }

    void Update()
    {

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            Managers.Resource.Destroy(gameObject);
        }
    }

    void OverScreen()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        Managers.Resource.Destroy(gameObject);
    }


}