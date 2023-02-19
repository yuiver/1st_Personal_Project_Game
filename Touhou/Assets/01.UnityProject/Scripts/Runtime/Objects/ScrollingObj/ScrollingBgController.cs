using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBgController : ScrollingObjController
{
    public override void Start()
    {
        base.Start();
    }       // Start()

    public override void Update()
    {
        base.Update();
    }       // Update()

    protected override void InitObjsPosition()
    {
        base.InitObjsPosition();

        float horizonPos =
            objPrefabSize.y * (scrollingObjCount - 1) * (-1) * 0.5f;
        for (int i = 0; i < scrollingObjCount; i++)
        {
            scrollingPool[i].SetLocalPos(0f, horizonPos, 0f);
            horizonPos = horizonPos + objPrefabSize.y;
        }
    }       // InitObjsPosition()

    protected override void RepositionFirstObj()
    {
        base.RepositionFirstObj();

        float lastScrObjCurrentYPos = scrollingPool[scrollingObjCount - 1].transform.localPosition.y;
        if (lastScrObjCurrentYPos <= objPrefabSize.y * 0.5f)
        {
            float lastScrObjInitYPos =
                Mathf.Floor(scrollingObjCount * 0.5f) *
                objPrefabSize.y + (objPrefabSize.y * 0.48f);

            scrollingPool[0].SetLocalPos(0f, lastScrObjInitYPos, 0f);
            scrollingPool.Add(scrollingPool[0]);
            scrollingPool.RemoveAt(0);

        }
    }       // RepositionFirstObj()
}
