using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public Sprite[] levelSpirte;
    public static int LevelCount = 0;
    public Image cakeImage;
    // Start is called before the first frame update
    void Start()
    {
        cakeImage = gameObject.GetComponentMust<Image>();
        ChangeCakeSprite();
    }


    void ChangeCakeSprite()
    {
        switch (LevelCount)
        {
            case 0:
                cakeImage.sprite = levelSpirte[0];
                break;
            case 1:
                cakeImage.sprite = levelSpirte[1];
                break;
            case 2:
                cakeImage.sprite = levelSpirte[2];
                break;
            case 3:
                cakeImage.sprite = levelSpirte[3];
                break;
            default:
                break;
        }
    }
}
