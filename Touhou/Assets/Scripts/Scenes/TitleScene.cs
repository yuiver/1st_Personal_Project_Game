using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : BaseScene
{
    protected override void Init()
    { 
        base.Init();

        SceneType = Define.Scene.TitleScene;

        List<int> selectUI = new List<int>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.GamePlayScene);
        }
    }

    public void OnClickStart()
    {
        Managers.Scene.LoadScene(Define.Scene.GamePlayScene);
    }

    public void OnClickEnd()
    {
        Util.QuitThisGame();
    }
    
    public override void Clear()
    {
        Debug.Log("TitleScene Clear!");
    }

    private float parameter = 0f;

    IEnumerator updateTimer()
    {
        GameObject guage_Bar = Util.FindChild(GameObject.Find("Game_Objs"), "Loading_Guage_Bar", true);
        Image guage_Bar_img = guage_Bar.GetComponentMust<Image>();
        while (parameter < 1)
        {
            guage_Bar_img.fillAmount = parameter;
            // { 시간이 1초씩 흘러가는 로직
            yield return new WaitForSeconds(1.0f);
            // } 시간이 1초씩 흘러가는 로직
            if (parameter < 1.0f)
            {
                parameter += 0.0001f;
            }
            else
            {
                Managers.Scene.LoadScene(Define.Scene.GamePlayScene);
            }
        }
    }
}
