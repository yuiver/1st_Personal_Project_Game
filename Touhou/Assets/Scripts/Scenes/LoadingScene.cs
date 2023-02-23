using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.LoadingScene;
    }

    private void Update()
    {
        Invoke("nextScene", 2f);
    }

    public override void Clear()
    {
        Debug.Log("TitleScene Clear!");
    }

    private void nextScene()
    {
        Managers.Scene.LoadScene(Define.Scene.TitleScene);
        Managers.soundClear = false;
        Managers.Sound.Play("BGM/MainBgm", Define.Sound.Bgm);
    }
}
