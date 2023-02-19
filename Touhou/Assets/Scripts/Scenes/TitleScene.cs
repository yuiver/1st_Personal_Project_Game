using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : BaseScene
{
    int selectNumber = default;
    public GameObject start;
    public GameObject option;
    public GameObject quit;

    protected override void Init()
    { 
        base.Init();

        SceneType = Define.Scene.TitleScene;

        selectNumber = 1;
        start.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectNumber == 1) { selectNumber = 3; }
            else { selectNumber--; }
            ControlSwitch(selectNumber);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectNumber == 3) { selectNumber = 1; }
            else { selectNumber++; }
            ControlSwitch(selectNumber);
        }



        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (selectNumber)
            {
                case 1:
                    Managers.Scene.LoadScene(Define.Scene.LevelSelectScene);
                    break;
                case 2:
                    break;
                case 3:
                    Util.QuitThisGame();
                    break;
                default:
                    break;
            }
        }
    }    
    public override void Clear()
    {
        Util.Log("TitleScene Clear!");
    }

    void ControlSwitch(int num)
    {


        bool _start = false;
        bool _option = false;
        bool _quit = false;

        if (num == 1){ _start = true; }

        if (num == 2) { _option = true; }

        if (num == 3) { _quit = true; }

        start.SetActive(_start);
        option.SetActive(_option);
        quit.SetActive(_quit);
       
    }
}
