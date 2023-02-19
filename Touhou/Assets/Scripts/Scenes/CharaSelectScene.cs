using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class CharaSelectScene : BaseScene
{
    public static int setChara = default;

    int selectNumber = default;

    public GameObject reimu;
    public GameObject marisa;
    public GameObject sakuya;
    public GameObject youmu;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.CharaSelectScene;

        setChara = default;
        selectNumber = 1;
        reimu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectNumber == 1) { selectNumber = 4; }
            else { selectNumber--; }
            ControlSwitch(selectNumber);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectNumber == 4) { selectNumber = 1; }
            else { selectNumber++; }
            ControlSwitch(selectNumber);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            setChara = selectNumber;
            Managers.Scene.LoadScene(Define.Scene.GamePlayScene);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Managers.Scene.LoadScene(Define.Scene.LevelSelectScene);
        }
    }
    public override void Clear()
    {
        Debug.Log("CharaSelectScene Clear!");
    }

    void ControlSwitch(int num)
    {
        bool _reimu = false;
        bool _marisa = false;
        bool _sakuya = false;
        bool _youmu = false;

        if (num == 1) { _reimu = true; }

        if (num == 2) { _marisa = true; }

        if (num == 3) { _sakuya = true; }

        if (num == 4) { _youmu = true; }

        reimu.SetActive(_reimu);
        marisa.SetActive(_marisa);
        sakuya.SetActive(_sakuya);
        youmu.SetActive(_youmu);
    }
}
