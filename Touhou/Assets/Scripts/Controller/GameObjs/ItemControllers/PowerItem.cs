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
        
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Graze"))
        {
            Managers.Resource.Destroy(gameObject);
            if (PlayerController.power >= 127)
            {
                PlayerController.power++;
            }
        }
    }
}
