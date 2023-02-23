using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided : MonoBehaviour
{
    public GameObject enemy;
    public GameObject bullet;

    float speed = 10.0f;
    # region 적으로 박는 탄환 (유도탄) 
    Vector3 bulletDir;
    Vector3 norDir;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bulletDir = new Vector3(enemy.transform.position.x - bullet.transform.position.x, enemy.transform.position.y - bullet.transform.position.y, 0f);
        norDir = bulletDir.normalized;
        this.gameObject.transform.Translate(norDir * Time.deltaTime * speed);
    }
    # endregion
}
