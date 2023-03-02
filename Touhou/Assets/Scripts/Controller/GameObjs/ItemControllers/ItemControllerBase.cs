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
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Graze"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    protected virtual void OverScreen()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
