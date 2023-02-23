using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonTest : MonoBehaviour
{
    public List<GameObject> enemies;

    public GameObject GetClosestEnemy(Vector3 playerPos)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPos, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
