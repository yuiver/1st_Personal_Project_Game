using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeController : MonoBehaviour
{

    public Sprite[] cakeSpirte;
    public static int cakeCount = default;
    public Image cakeImage;
    // Start is called before the first frame update
    void Start()
    {
        cakeCount = 9;
        cakeImage = gameObject.GetComponentMust<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCakeSprite();
    }

    void ChangeCakeSprite()
    {
        switch (cakeCount)
        {
            case 0:
                cakeImage.sprite = cakeSpirte[0];
                break;
            case 1:
                cakeImage.sprite = cakeSpirte[1];
                break;
            case 2:
                cakeImage.sprite = cakeSpirte[2];
                break;
            case 3:
                cakeImage.sprite = cakeSpirte[3];
                break;
            case 4:
                cakeImage.sprite = cakeSpirte[4];
                break;
            case 5:
                cakeImage.sprite = cakeSpirte[5];
                break;
            case 6:
                cakeImage.sprite = cakeSpirte[6];
                break;
            case 7:
                cakeImage.sprite = cakeSpirte[7];
                break;
            case 8:
                cakeImage.sprite = cakeSpirte[8];
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cakeCount--;
    }
}
