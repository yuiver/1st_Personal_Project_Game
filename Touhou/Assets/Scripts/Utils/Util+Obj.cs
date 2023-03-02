using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public static partial class Util
{
    public static GameObject FindChildObj(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for (int i=0; i< targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);

                // 방어로직
                if(searchResult == null || searchResult == default) { /* Pass */ }
                else { return searchResult; }
            }
        }       // loop


        return searchResult;
    }       // FindChildObj()


    //! ���� ��Ʈ ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach(GameObject rootObj in rootObjs_)
        {
            if(rootObj.name.Equals(objName_))
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }       // loop

        return targetObj_;
    }       // GetRootObj()

    
    public static RectTransform GetRect(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>();
    }       // GetRect()

    
    public static Vector2 GetRectSizeDelta(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>().sizeDelta;
    }       // GetRectSizeDelta()

    
    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }       // GetActiveScene()

    
    public static void SetLocalPos(this GameObject obj_, 
        float x, float y, float z)
    {
        obj_.transform.localPosition = new Vector3(x, y, z);
    }       // SetLocalPos()

    
    public static void AddLocalPos(this GameObject obj_, 
        float x, float y, float z)
    {
        obj_.transform.localPosition = 
            obj_.transform.localPosition + new Vector3(x, y, z);
    }       // AddLocalPos()

    
    public static void AddAnchoredPos(this GameObject obj_,
        float x, float y)
    {
        obj_.GetRect().anchoredPosition += new Vector2(x, y);
    }       // AddAnchoredPos()

    
    public static void AddAnchoredPos(this GameObject obj_,
        Vector2 position2D)
    {
        obj_.GetRect().anchoredPosition += position2D;
    }       // AddAnchoredPos()

   
    public static void Translate(this Transform transform_, Vector2 moveVector)
    {
        transform_.Translate(moveVector.x, moveVector.y, 0f);
    }       // Translate()
            
    public static T GetComponentMust<T>(this GameObject obj)
    {
        T component_ = obj.GetComponent<T>();

        Util.Assert(component_.IsValid<T>() != false,
            string.Format("{0}에서 {1}을(를) 찾을 수 없습니다.",
            obj.name, component_.GetType().Name));

        return component_;
    }       // GetComponentMust()
    
    public static T GetComponentMust<T>(this GameObject obj , string objName)
    {
        T component_ = obj.FindChildObj(objName).GetComponent<T>();

        Util.Assert(component_.IsValid<T>() != false, 
            string.Format("{0}에서 {1}을(를) 찾을 수 없습니다.",
            obj.name, component_.GetType().Name));

        return component_;
    }       // GetComponentMust()

    
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }       // CreateObj()

    public static void ImageAlphaChange(GameObject target, float alphaParamMaxIs1)
    {
        Image image = target.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, alphaParamMaxIs1);
    }

    public static IEnumerator ImageAlphaChangeDelayDown(GameObject target)
    {
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.8f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.6f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.4f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.2f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.0f);

    }
    public static IEnumerator ImageAlphaChangeDelayUp(GameObject target)
    {
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.2f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.4f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.6f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 0.8f);
        yield return new WaitForSeconds(0.3f);
        ImageAlphaChange(target, 1.0f);

    }
}
