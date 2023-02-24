using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GrazeCheckController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("EnemyBullet"))
        {
            PlayerController.graze++;
        }
    }
}
