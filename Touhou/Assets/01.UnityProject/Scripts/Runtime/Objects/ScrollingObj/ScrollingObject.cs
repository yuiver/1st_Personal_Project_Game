using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollingSpeed = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePlayScene.isGameOver == false)
        {
            transform.Translate(Vector2.left * scrollingSpeed * Time.deltaTime);
        }
    }       // Update()
}
