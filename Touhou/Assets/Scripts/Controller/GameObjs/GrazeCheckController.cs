using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GrazeCheckController : MonoBehaviour
{

    private void Update()
    {
        gameObject.transform.localPosition = PlayerController.playerPosition;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Util.ImageAlphaChange(this.gameObject, 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Util.ImageAlphaChange(this.gameObject, 0.0f);
        }
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            PlayerController.graze++;
        }
    }

}
