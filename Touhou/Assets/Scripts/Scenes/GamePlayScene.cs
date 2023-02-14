using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class GamePlayScene : BaseScene
{
    public static GameObject target;
    public static GameObject origin;

    public static int inGameAnt = 0;
    public static int antCount = 0;
    public static int gameOverCount = 0;



    private int deathCount = 0;
    //이새끼는 데이터 매니저에서 받아오는거니 나중에
    private Dictionary<int, Stat> dict = default;

    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    Transform parent_Tf = default;

    public Transform catPoolRoot { get; set; }
    public Transform bulletPoolRoot { get; set; }

    protected override void Init()
    { 
        base.Init();

        dict = Managers.Data.StatDict;
        SceneType = Define.Scene.GamePlayScene;

        //Managers.Resource.Instantiate("UI/UI_Button");

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.UI.ShowPopupUI<UI_Button>();

        //UI_Button ui =
        //Managers.UI.ClosePopupUI(ui);

        //캐싱하기 위해 경로를 잡아서 Transfrom주소를 캐싱한다.
        gameObjParent = GameObject.Find("Game_Objs");
        parent_Tf = gameObjParent.gameObject.transform;


        //오브젝트를 한번에 foreach로 팝하기 위해서 리스트에 저장한다.
        List<GameObject> antList = new List<GameObject>();
        List<GameObject> bulletList = new List<GameObject>();

        catPoolRoot = new GameObject().transform;
        catPoolRoot.SetParent(parent_Tf,false);
        catPoolRoot.name = $"@{Define.ENEMY_PREFAB_PATH}_Obj";

        bulletPoolRoot = new GameObject().transform;
        bulletPoolRoot.SetParent(parent_Tf, false);
        bulletPoolRoot.name = $"@{Define.BULLET_PREFAB_PATH}_Obj";

        GamePlayScene.target = GameObject.FindWithTag("Cake");
        GamePlayScene.origin = GameObject.FindWithTag("Nest");


        for (int i = 0; i < 8; i++)
        {
            antList.Add(Managers.Resource.Instantiate($"Game_Objs/{Define.ENEMY_PREFAB_PATH}", catPoolRoot));
        }
        foreach (GameObject antObj in antList)
        {
            Managers.Resource.Destroy(antObj);
        }

        for (int i = 0; i < 150; i++)
        {
            bulletList.Add(Managers.Resource.Instantiate($"Game_Objs/{Define.BULLET_PREFAB_PATH}", bulletPoolRoot));
        }
        foreach (GameObject bulletObj in bulletList)
        {
            Managers.Resource.Destroy(bulletObj);
        }

    }


    private void Update()
    {
        if(inGameAnt < 8)
        {
            GameObject go = Managers.Resource.Instantiate($"Game_Objs/{Define.ENEMY_PREFAB_PATH}", catPoolRoot);
            go.transform.localPosition = origin.transform.localPosition;
            inGameAnt++;
            antCount++;

            //-1024 1024 : 좌상 1
            //-1024 -616 : 좌하 끝
            //1024 1024 : 우상 끝
            //1024 -616 : 우하 끝 
            
            //RactTransform 에서 좌료를 받아오면 된다
            //RectTransform. = new Vector3(100f, 100f, 0f);
            //RectTransform
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject go =
            Managers.Resource.Instantiate($"Game_Objs/{Define.BULLET_PREFAB_PATH}", bulletPoolRoot);
            //go.transform.localScale = Vector3.one;
            //go.transform.localPosition = Vector3.zero;
            //go.transform.localPosition = new Vector2(0f, 0f);
        }
        if (gameOverCount == 8)
        {
            IsGameOver();
        }
    }
    private void IsGameOver()
    {
        if (Define.CAN_RETRY_COUNT > deathCount)
        {
            //대충 보스를 제외한걸 오프하고 뒤진 패널티를 적용하고 부활핞다. 
        }
        else
        {
            //Call GameOver UI and player can Selcet go Title Retry or
        }
        //게임 오버 UI를 호출하면 된다. 최대 부활가능 3회 
    }


    public override void Clear()
    {
     
    }
}
