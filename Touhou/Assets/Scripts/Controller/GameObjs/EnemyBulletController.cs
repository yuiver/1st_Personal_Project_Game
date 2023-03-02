using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBulletController : MonoBehaviour
{
    

    void Update()
    {
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
        //if (PlayerController.playerHitOn == true)
        //{
        //    PlayerController.playerHitOn = false;
        //    StartCoroutine(HitAndChangeRgb(gameObject));
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            Managers.Resource.Destroy(gameObject);
        }
        else if (collision.CompareTag("Spell"))
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            OverScreen();
        }
    }

    void OverScreen()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        Managers.Resource.Destroy(gameObject);
    }

    //IEnumerator HitAndChangeRgb(GameObject Target)
    //{
    //    Image image = Target.GetComponent<Image>();
    //    image.color = new Color(255.0f, 0.0f, 0.0f, 1.0f);
    //    yield return new WaitForSeconds(1.0f);
    //    image.color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
    //}
}
