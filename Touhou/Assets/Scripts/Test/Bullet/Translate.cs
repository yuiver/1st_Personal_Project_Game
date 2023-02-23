using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject bullet;

    float speed = 10.0f;

    # region 직선으로 박는 탄환 (조준탄) 
    Vector3 bulletDir;
    Vector3 norDir;

    // Start is called before the first frame update
    void Start()
    {
        bulletDir = new Vector3(enemy.transform.position.x - player.transform.position.x, enemy.transform.position.y - player.transform.position.y, 0f);
        norDir = bulletDir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(norDir * Time.deltaTime * speed);
    }
    # endregion
    //방향탄은 너무 간단한 것이다 
    //this.gameObject.transform.Translate(testDir * Time.deltaTime * speed); 에서 test 각도의 Vector2 만 계산하면 된다.
    //X 좌표 : cos(3.141592 / 180  )  Y 좌표 :
}
