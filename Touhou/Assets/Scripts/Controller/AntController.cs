using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public float speed = default;
    bool IsAntHaveCake = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 100.0f;
        IsAntHaveCake = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ************************************************
        // ***          Take cake logic / Ant           ***
        // ************************************************
        // #TakeCake
        if (IsAntHaveCake == true)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, GamePlayScene.origin.transform.localPosition, speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, GamePlayScene.target.transform.localPosition , speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsAntHaveCake == false && other.transform.tag == "Cake")
        {
            IsAntHaveCake = true;
            CakeController.cakeCount--;
        }
        if (IsAntHaveCake == true && other.transform.tag == "Nest")
        {
            IsAntHaveCake = false;
            GamePlayScene.gameOverCount++;
        }
    }
    private void AntDied()
    {

        Managers.Resource.Destroy(gameObject);
        CakeController.cakeCount++;
        if (IsAntHaveCake == false)
        {
            Managers.Resource.Destroy(gameObject);
        }
        if (IsAntHaveCake == true)
        {
            Managers.Resource.Destroy(gameObject);
            GamePlayScene.gameOverCount++;
        }

    }
}
