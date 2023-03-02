using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : ItemControllerBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Graze"))
        {
            Managers.Resource.Destroy(gameObject);
            if (PlayerController.power <= 127)
            {
                PlayerController.power++;
            }
        }
    }
}
