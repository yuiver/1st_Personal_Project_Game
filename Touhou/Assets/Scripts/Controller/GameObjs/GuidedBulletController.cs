using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBulletController : MonoBehaviour
{

    private GameObject bullet;
    public GameObject enemy;

    private bool targetEnemyDead = false;

    float speed = 30.0f;
    Vector3 bulletDir;
    Vector3 norDir;

    public Vector3 bulletPosition = default;
    GameObject targetEnemy = default;

    public GameObject GetClosestEnemy(Vector3 bulletPosition)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in GamePlayScene.enemyList)
        {
            float distance = Vector3.Distance(bulletPosition, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }


    void Start()
    {
        bullet = this.gameObject;
    }

    void OnEnable()
    {
    }

    void Update()
    {
        bulletPosition = gameObject.transform.position;

        if (targetEnemyDead == false)
        {
            targetEnemy = GetClosestEnemy(bulletPosition);
        }

        //유도탄
        if (targetEnemy != null)
        {
            float discheck = 0;

            bulletDir = new Vector3(targetEnemy.transform.position.x - bullet.transform.position.x, targetEnemy.transform.position.y - bullet.transform.position.y, 0f);
            norDir = bulletDir.normalized;
            discheck = Vector3.Distance(targetEnemy.transform.position, bullet.transform.position);
            if (discheck < 8.0f)
            { bullet.transform.Translate(norDir * Time.deltaTime * speed); }
        }

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
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }

}