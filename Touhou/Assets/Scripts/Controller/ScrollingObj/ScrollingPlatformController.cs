using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlatformController : ScrollingObjController
{
    private bool isStart = false;

    // LEGACY:
    //protected float prefabYPos = default;

    public override void Start()
    {
        base.Start();

        isStart = true;

        // LEGACY:
        //prefabYPos = objPrefab.transform.localPosition.y;
    }       // Start()

    public override void Update()
    {
        base.Update();
    }       // Update()

    protected override void InitObjsPosition()
    {
        base.InitObjsPosition();

        Vector2 posOffset = Vector2.zero;

        float xPos =
            objPrefabSize.x * (scrollingObjCount - 1) * (-1) * 0.5f;
        float yPos = prefabXPos;
        for (int i = 0; i < scrollingObjCount; i++)
        {
            scrollingPool[i].SetLocalPos(xPos, yPos, 0f);

            posOffset = GetRandomPosOffset();
            if(isStart == true && i == 1)
            {
                xPos = xPos + objPrefabSize.x;
                isStart = false;
            }
            else
            {
                xPos = xPos + objPrefabSize.x + posOffset.x;
            }
            yPos = prefabXPos + posOffset.x;
        }       // loop
        // }
    }       // InitObjsPosition()

    protected override void RepositionFirstObj()
    {
        base.RepositionFirstObj();

        // ��ũ�Ѹ� Ǯ�� ù��° ������Ʈ�� ���������� �������Ŵ� �ϴ� ����
        float lastScrObjCurrentXPos = scrollingPool[scrollingObjCount - 1].transform.localPosition.x;
        if (lastScrObjCurrentXPos <= objPrefabSize.x * 0.5f)
        {
            Vector2 posOffset = Vector2.zero;
            posOffset = GetRandomPosOffset();

            float lastScrObjInitXPos =
                Mathf.Floor(scrollingObjCount * 0.5f) *
                objPrefabSize.x + (objPrefabSize.x * 0.5f);

            scrollingPool[0].SetLocalPos(
                lastScrObjInitXPos + posOffset.x, prefabXPos + posOffset.x, 0f);
            scrollingPool.Add(scrollingPool[0]);
            scrollingPool.RemoveAt(0);

            // DEBUG: 
            //GFunc.Log($"List Pos: {scrollingPool[0].transform.localPosition}, " +
            //    $"{scrollingPool[2].transform.localPosition}");

        }
    }       // RepositionFirstObj()

    //! ������ ������ �������� �����ϴ� �Լ�
    private Vector2 GetRandomPosOffset()
    {
        Vector2 offset = Vector2.zero;
        offset.x = Random.Range(50f, 300f);
        offset.y = Random.Range(-80f, 30f);

        return offset;
    }       // GetRandomPosOffset()
}
