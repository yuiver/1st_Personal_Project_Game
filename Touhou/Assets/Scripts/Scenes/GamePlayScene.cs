using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.UI.Image;

public class GamePlayScene : BaseScene
{
    //탄환의 추척을 위해 만든 변수 private로 교체할것
    public static GameObject target;
    public static GameObject origin;
    public GameObject instance;

    //인게임 정지키로 호출하는 메뉴를 컨트롤 하기위한 변수
    public GameObject escMenu;
    public GameObject gameContinue;
    public GameObject goTitleScene;
    public GameObject restartGame;

    public GameObject reallyMenu;
    public GameObject yes;
    public GameObject no;

    private bool pauseGame = default; //이게 정지키를 눌렀는지 체크하는 불변수
    private bool reallySelect = default;
    private int selectNumber = default;
    private int selectReally = default;

    //게임의 승패를 체크하기 위한 변수를 지정
    public static int gameOverCount = 0;
    public static bool isGameOver = false;

    private int deathCount = 0;


    //오브젝트 풀링을 위한 변수들 여기서는 적유닛을 풀링하기위해 만들었다.
    GameObject gameObjParent = default; //부모
    Transform parent_Tf = default; //부모위치 캐싱


    public Transform enemyPoolRoot { get; set; }
    public Transform enemyBulletPoolRoot { get; set; }


    public static List<GameObject> enemyList = new List<GameObject>();
    public static List<GameObject> enemyBulletList = new List<GameObject>();
    private float enemySpeed = 2f;

    protected override void Init()
    { 
        base.Init();
        SceneType = Define.Scene.GamePlayScene;

        //BGM을 파일경로로 탐색해서 가져옴
        Managers.Sound.Play("BGM/Stage1-1", Define.Sound.Bgm);
        //게임 오버인지 체크하는 변수를 한번 초기화해준다.
        isGameOver = false;
        
        //이게 정지키를 눌러서 호출할 오브젝트를 위해 초기화
        pauseGame = false;
        reallySelect = false;
        selectNumber = 1;
        selectReally = 1;


        //캐싱하기 위해 경로를 잡아서 Transfrom주소를 캐싱한다.
        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;
        //오브젝트를 한번에 foreach로 팝하기 위해서 리스트에 저장한다.
        //List<GameObject> enemyList = new List<GameObject>();

        enemyPoolRoot = new GameObject().transform;
        enemyPoolRoot.SetParent(parent_Tf,false);
        enemyPoolRoot.name = $"@{Define.ENEMY_PREFAB_PATH}_Obj";

        for (int i = 0; i < 30; i++)
        {
            enemyList.Add(Managers.Resource.Instantiate($"gameObjs/{Define.ENEMY_PREFAB_PATH}", enemyPoolRoot));
        }
        foreach (GameObject enemyObj in enemyList)
        {
            Managers.Resource.Destroy(enemyObj);
        }

        enemyBulletPoolRoot = new GameObject().transform;
        enemyBulletPoolRoot.SetParent(parent_Tf, false);
        enemyBulletPoolRoot.name = $"@{Define.ENEMY_BULLET_PREFAB_PATH}_Obj";

        for (int i = 0; i < 1000; i++)
        {
            enemyBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_BULLET_PREFAB_PATH}", enemyBulletPoolRoot));
        }
        foreach (GameObject enemyBulletObj in enemyBulletList)
        {
            Managers.Resource.Destroy(enemyBulletObj);
        }

        pauseGame = false;

    }

    private void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }


    private void Update()
    {
        //일시 정지 하는데 여기서 UI도 띄우자
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame == false)
            {

                Managers.Sound.Play("SE/se_pause");
                Util.Log("시간정지!");
                Time.timeScale = 0;
                pauseGame = true;
                //여기서 UI호출도 해야함
                escMenu.SetActive(true);
                EscControlSwitch(selectNumber);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pauseGame == true && reallySelect == false)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectNumber == 1) { selectNumber = 3; }
                else { selectNumber--; }
                EscControlSwitch(selectNumber);
            }
            else if (pauseGame == true && reallySelect == true)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectReally == 1) { selectReally = 2; }
                else { selectReally--; }
                ReallyControlSwitch(selectReally);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pauseGame == true && reallySelect == false)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectNumber == 3) { selectNumber = 1; }
                else { selectNumber++; }
                EscControlSwitch(selectNumber);
            }
            else if (pauseGame == true && reallySelect == true)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectReally == 2) { selectReally = 1; }
                else { selectReally++; }
                ReallyControlSwitch(selectReally);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (pauseGame == true && reallySelect == false)
            {
                Managers.Sound.Play("SE/se_ok00");
                switch (selectNumber)
                {
                    //게임 재개
                    case 1:
                        StopGameContinue();
                        break;
                    //그만하고 돌아가기
                    case 2:
                        ReallyActive(true);
                        break;
                    //처음부터 다시 시작
                    case 3:
                        ReallyActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (pauseGame == true && reallySelect == true)
            {
                Managers.Sound.Play("SE/se_ok00");
                switch (selectReally)
                {
                    //네
                    case 1:
                        ReallyControl(selectNumber);
                        break;
                    //아니요
                    case 2:
                        ReallyActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (pauseGame == true && reallySelect == true)
            {
                ReallyActive(false);
            }
        }

        //    if(inGameEnemy < 20)
        //    {
        //        GameObject go = Managers.Resource.Instantiate($"GameObjs/{Define.ENEMY_PREFAB_PATH}", enemyPoolRoot);
        //        go.transform.localPosition = origin.transform.localPosition;
        //        inGameEnemy++;
        //        antCount++;

        //        //RactTransform 에서 좌료를 받아오면 된다
        //        //RectTransform. = new Vector3(100f, 100f, 0f);
        //        //RectTransform
        //    }

        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        GameObject go =
        //        Managers.Resource.Instantiate($"GameObjs/{Define.BULLET_PREFAB_PATH}", bulletPoolRoot);
        //        //go.transform.localScale = Vector3.one;
        //        //go.transform.localPosition = Vector3.zero;
        //        //go.transform.localPosition = new Vector2(0f, 0f);
        //    }
        //    if (gameOverCount == 8)
        //    {
        //        IsGameOver();
        //    }
        //}
        //private void IsGameOver()
        //{
        //    if (Define.CAN_RETRY_COUNT > deathCount)
        //    {
        //        //대충 보스를 제외한걸 오프하고 뒤진 패널티를 적용하고 부활핞다. 
        //    }
        //    else
        //    {
        //        //Call GameOver UI and player can Selcet go Title Retry or
        //    }
        //    //게임 오버 UI를 호출하면 된다. 최대 부활가능 3회 
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            InstanceEnemy(Vector2.right);
            //0.5초 대기
            yield return new WaitForSeconds(5.0f);
        }
    }


    void InstanceEnemy(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"gameObjs/{Define.ENEMY_PREFAB_PATH}", enemyPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        go.transform.localPosition = instance.transform.localPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * enemySpeed;
    }


    public override void Clear()
    {
     
    }

    //플레이어의 공격을 생성하는 단계

    private void GameOver()
    {

    }

    void EscControlSwitch(int num)
    {


        bool _gameContinue = false;
        bool _goTitleScene = false;
        bool _restartGame = false;

        if (num == 1) { _gameContinue = true; }

        if (num == 2) { _goTitleScene = true; }

        if (num == 3) { _restartGame = true; }

        ImageAlphaChanger(gameContinue, _gameContinue);
        ImageAlphaChanger(goTitleScene, _goTitleScene);
        ImageAlphaChanger(restartGame, _restartGame);
    }

        void ReallyControlSwitch(int num)
    {
        bool _yes = false;
        bool _no = false;

        if (num == 1) { _yes = true; }

        if (num == 2) { _no = true; }

        ImageAlphaChanger(yes, _yes);
        ImageAlphaChanger(no, _no);
    }

    void ImageAlphaChanger(GameObject target , bool onOff)
    {
        Image image = target.GetComponent<Image>();

        if (onOff == true)
        {
            image.color = new Color (image.color.r, image.color.g, image.color.b, 1f);
        }

        if (onOff == false)
        {
            image.color = new Color (image.color.r, image.color.g, image.color.b, 0.5f);
        }

    }

    void StopGameContinue()
    {
        Managers.Sound.Play("SE/se_cancel00");
        Util.Log("시간정지해제");
        Time.timeScale = 1;
        pauseGame = false;
        escMenu.SetActive(false);
    }

    void ReallyControl(int number)
    {
        switch (number)
        {
            //네
            case 2:
                Managers.Scene.LoadScene(Define.Scene.LoadingScene);
                break;
            //아니요
            case 3:
                Managers.Scene.LoadScene(Define.Scene.GamePlayScene);
                break;
            default:
                break;
        }
    }

    void ReallyActive(bool select)
    {
        selectReally = 1;
        reallySelect = select;
        reallyMenu.SetActive(select);
        ReallyControlSwitch(selectReally);
    }
    

}
