using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScene : BaseScene
{
    public static int level = default;

    int selectNumber = default;

    public GameObject easy;
    public GameObject normal;
    public GameObject hard;
    public GameObject lunatic;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.LevelSelectScene;

        level = default;
        selectNumber = 1;
        easy.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectNumber == 1) { selectNumber = 4; }
            else { selectNumber--; }
            ControlSwitch(selectNumber);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectNumber == 4) { selectNumber = 1; }
            else { selectNumber++; }
            ControlSwitch(selectNumber);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            level = selectNumber;
            Managers.Scene.LoadScene(Define.Scene.CharaSelectScene);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Managers.Scene.LoadScene(Define.Scene.TitleScene);
        }
    }
    public override void Clear()
    {
        Util.Log("LevelSelectScene Clear!");
    }

    void ControlSwitch(int num)
    {
        bool _start = false;
        bool _option = false;
        bool _quit = false;
        bool _lunatic = false;

        if (num == 1) { _start = true; }

        if (num == 2) { _option = true; }

        if (num == 3) { _quit = true; }

        if (num == 4) { _lunatic = true; }

        easy.SetActive(_start);
        normal.SetActive(_option);
        hard.SetActive(_quit);
        lunatic.SetActive(_lunatic);
    }
}
