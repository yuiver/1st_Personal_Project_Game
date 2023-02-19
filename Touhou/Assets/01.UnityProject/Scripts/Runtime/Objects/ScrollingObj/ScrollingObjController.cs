using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjController : MonoBehaviour
{
    public string prefabName = default;
    public int scrollingObjCount = default;

    public float scrollingSpeed = default;

    protected GameObject objPrefab = default;
    protected Vector2 objPrefabSize = default;
    protected List<GameObject> scrollingPool = default;

    protected float prefabXPos = default;

    // Start is called before the first frame update
    public virtual void Start()
    {
        objPrefab = gameObject.FindChildObj(prefabName);
        scrollingPool = new List<GameObject>();
        Util.Assert(objPrefab != null || objPrefab != default);

        objPrefabSize = objPrefab.GetRectSizeDelta();
        prefabXPos = objPrefab.transform.localPosition.x;

        GameObject tempObj = default;
        if(scrollingPool.Count <= 0)
        {
            for (int i = 0; i < scrollingObjCount; i++)
            {
                tempObj = Instantiate(objPrefab,
                    objPrefab.transform.position,
                    objPrefab.transform.rotation, transform);

                scrollingPool.Add(tempObj);
                tempObj = default;
            }
        }

        objPrefab.SetActive(false);

        InitObjsPosition();

    }       // Start()

    // Update is called once per frame
    public virtual void Update()
    {
        if(scrollingPool == default || scrollingPool.Count <= 0)
        {
            return;
        }

        if (GamePlayScene.isGameOver == false)
        {
            for (int i = 0; i < scrollingObjCount; i++)
            {
                scrollingPool[i].AddLocalPos(scrollingSpeed * Time.deltaTime * 0f, (-1f), 0f);
            }

            RepositionFirstObj();
        }
    }       // Update()

    protected virtual void InitObjsPosition()
    {
        /* Do something */
    }       // InitObjsPosition()

    protected virtual void RepositionFirstObj()
    {
        /* Do something */
    }       // RepositionFirstObj()
}
