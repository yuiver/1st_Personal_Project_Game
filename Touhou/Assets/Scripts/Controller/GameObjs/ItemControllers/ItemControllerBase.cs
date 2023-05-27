using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControllerBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (gameObject.transform.localPosition.x >= 310.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.x <= -660.0f)
        {
            OverScreen();
        }
        if (gameObject.transform.localPosition.y >= 510.0f)
        {
            OverScreen();
        }
        else if (gameObject.transform.localPosition.y <= -510.0f)
        {
            OverScreen();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Graze"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    void OverScreen()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        Managers.Resource.Destroy(gameObject);
    }

}
