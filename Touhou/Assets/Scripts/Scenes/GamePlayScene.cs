using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class GamePlayScene : BaseScene
{
    #region variable
    //탄환의 추척을 위해 만든 변수 private로 교체할것
    public static GameObject target;
    public static GameObject origin;
    public GameObject instance;
    Vector3 bossInstPoint = new Vector3(606, 348, 100);
    Vector3 instPointTopLeft = new Vector3(603.58f, 348.08f, 100.00f);
    Vector3 instPointTopMidLeft = new Vector3(606.06f, 348.08f, 100.00f);
    Vector3 instPointTop = new Vector3(608.25f, 348.33f, 100.00f);
    Vector3 instPointTopRight = new Vector3(612.38f, 348.33f, 100.00f);
    Vector3 instPointTopMidRight = new Vector3(610.33f, 348.33f, 100.00f);

    Vector3 instPointLeft01 = new Vector3(603.45f, 347.16f, 100.00f);
    Vector3 instPointLeft02 = new Vector3(603.45f, 346.66f, 100.00f);
    Vector3 instPointLeft03 = new Vector3(603.45f, 346.16f, 100.00f);
    Vector3 instPointLeft04 = new Vector3(603.45f, 343.14f, 100.00f);
    Vector3 instPointLeft05 = new Vector3(603.45f, 345.66f, 100.00f);
    Vector3 instPointLeft06 = new Vector3(603.45f, 345.16f, 100.00f);
    Vector3 instPointLeft07 = new Vector3(603.45f, 344.66f, 100.00f);
    Vector3 instPointLeft08 = new Vector3(603.45f, 344.16f, 100.00f);

    Vector3 instPointRight01 = new Vector3(612.47f, 347.16f, 100.00f);
    Vector3 instPointRight02 = new Vector3(612.47f, 346.66f, 100.00f);
    Vector3 instPointRight03 = new Vector3(612.47f, 346.16f, 100.00f);
    Vector3 instPointRight04 = new Vector3(612.47f, 345.66f, 100.00f);
    Vector3 instPointRight05 = new Vector3(612.47f, 345.16f, 100.00f);
    Vector3 instPointRight06 = new Vector3(612.47f, 344.66f, 100.00f);
    Vector3 instPointRight07 = new Vector3(612.47f, 344.16f, 100.00f);



    //인게임 정지키로 호출하는 메뉴를 컨트롤 하기위한 변수
    public GameObject escMenu;
    public GameObject gameContinue;
    public GameObject goTitleScene;
    public GameObject restartGame;

    public GameObject reallyMenu;
    public GameObject yes;
    public GameObject no;

    public GameObject retryMenu;
    public GameObject RetryYes;
    public GameObject RetryNo;

    public GameObject stage1Story;
    public GameObject stageClear;

    private bool storyOn = default;
    private bool pauseGame = default; //이게 정지키를 눌렀는지 체크하는 불변수
    private bool reallySelect = default;
    private int selectNumber = default;
    private int selectReally = default;
    private int selectRetry = default;
    private int retryCount = default;

    //게임의 승패를 체크하기 위한 변수를 지정
    public static int gameOverCount = 0;
    public static bool isGameOver = false;

    private int deathCount = 0;


    //오브젝트 풀링을 위한 변수들 여기서는 적유닛을 풀링하기위해 만들었다.
    GameObject gameObjParent = default; //부모
    Transform parent_Tf = default; //부모위치 캐싱


    public Transform enemyPoolRoot { get; set; }
    public Transform enemyBulletPoolRoot { get; set; }
    public Transform bossPoolRoot { get; set; }
    public Transform powerItemPoolRoot { get; set; }
    public Transform pointItemPoolRoot { get; set; }


    public static List<GameObject> enemyList = new List<GameObject>();
    public static List<GameObject> enemyBulletList = new List<GameObject>();
    public static List<GameObject> powerItemList = new List<GameObject>();
    public static List<GameObject> pointItemList = new List<GameObject>();
    public static List<GameObject> timeItemList = new List<GameObject>();
    public static List<GameObject> fullPowerList = new List<GameObject>();

    private float enemySpeed = 3.0f;
    private float bossSpeed = 1.5f;
    #endregion


    protected override void Init()
    { 
        base.Init();
        storyOn = false;
        retryCount = 1;
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
        selectRetry = 1;




        //캐싱하기 위해 경로를 잡아서 Transfrom주소를 캐싱한다.
        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;
        //오브젝트를 한번에 foreach로 팝하기 위해서 리스트에 저장한다.
        //List<GameObject> enemyList = new List<GameObject>();

        //여기서 적 유닛을 풀링
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

        //여기서 적의 총알을 풀링
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

        //여기서 파워 아이템을 풀링
        powerItemPoolRoot = new GameObject().transform;
        powerItemPoolRoot.SetParent(parent_Tf, false);
        powerItemPoolRoot.name = $"@{Define.ITEM_POWER}_Obj";
        for (int i = 0; i < 50; i++)
        {
            powerItemList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.ITEM_POWER}", powerItemPoolRoot));
        }
        foreach (GameObject powerItemObj in powerItemList)
        {
            Managers.Resource.Destroy(powerItemObj);
        }

        //여기서 포인트 아이템을 풀링
        pointItemPoolRoot = new GameObject().transform;
        pointItemPoolRoot.SetParent(parent_Tf, false);
        pointItemPoolRoot.name = $"@{Define.ITEM_POINT}_Obj";
        for (int i = 0; i < 50; i++)
        {
            pointItemList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.ITEM_POINT}", pointItemPoolRoot));
        }
        foreach (GameObject pointItemObj in pointItemList)
        {
            Managers.Resource.Destroy(pointItemObj);
        }

        //보스의 경로를 설정해줌
        bossPoolRoot = new GameObject().transform;
        bossPoolRoot.SetParent(parent_Tf, false);
        bossPoolRoot.name = $"@{Define.ENEMY_BOSS_STAGE_01}_Obj";



        pauseGame = false;

    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Stage1Play());
    }

    private void OnEnable()
    {
    }


    private void Update()
    {
        if (Stage1Boss.bossHp <= 0)
        {
            StartCoroutine(BossClear());
        }

    

        //재도전 할것인지 선택
        if (PlayerController.noLifeCheck)
        {
            Time.timeScale = 0;
            if (retryCount < 3)
            {
                retryMenu.SetActive(true);
                RetryControlSwitch(selectRetry);
            }
            else if (retryCount >= 3)
            {
                Managers.Scene.LoadScene(Define.Scene.TitleScene);
                Time.timeScale = 1;
                //이곳에 정산창 구현
            }
        }

        //일시 정지 하는데 여기서 UI도 띄우자
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame == false)
            {

                Managers.Sound.Play("SE/se_pause");
                Time.timeScale = 0;
                pauseGame = true;
                //여기서 UI호출도 해야함
                escMenu.SetActive(true);
                EscControlSwitch(selectNumber);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pauseGame == true && reallySelect == false && PlayerController.noLifeCheck == false)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectNumber == 1) { selectNumber = 3; }
                else { selectNumber--; }
                EscControlSwitch(selectNumber);
            }
            else if (pauseGame == true && reallySelect == true && PlayerController.noLifeCheck == false)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectReally == 1) { selectReally = 2; }
                else { selectReally--; }
                ReallyControlSwitch(selectReally);
            }
            else if (PlayerController.noLifeCheck == true)
            {
                Managers.Sound.Play("SE/se_select00");
                if (selectRetry == 1) { selectRetry = 2; }
                else { selectRetry--; }
                RetryControlSwitch(selectRetry);
            }
        }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (pauseGame == true && reallySelect == false && PlayerController.noLifeCheck == false)
                {
                    Managers.Sound.Play("SE/se_select00");
                    if (selectNumber == 3) { selectNumber = 1; }
                    else { selectNumber++; }
                    EscControlSwitch(selectNumber);
                }
                else if (pauseGame == true && reallySelect == true && PlayerController.noLifeCheck == false)
                {
                    Managers.Sound.Play("SE/se_select00");
                    if (selectReally == 2) { selectReally = 1; }
                    else { selectReally++; }
                    ReallyControlSwitch(selectReally);
                }
                else if (PlayerController.noLifeCheck == true)
                {
                    Managers.Sound.Play("SE/se_select00");
                    if (selectRetry == 2) { selectRetry = 1; }
                    else { selectRetry++; }
                    RetryControlSwitch(selectRetry);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (pauseGame == true && reallySelect == false && PlayerController.noLifeCheck == false)
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
                else if (pauseGame == true && reallySelect == true && PlayerController.noLifeCheck == false)
                {
                    Managers.Sound.Play("SE/se_ok00");
                    switch (selectReally)
                    {
                        //네
                        case 1:
                            RetryControl(selectNumber);
                            break;
                        //아니요
                        case 2:
                            ReallyActive(false);
                            break;
                        default:
                            break;
                    }
                }
                else if (PlayerController.noLifeCheck == true)
                {
                    Managers.Sound.Play("SE/se_ok00");
                    switch (selectRetry)
                    {
                        //네
                        case 1:
                            RetryGame();
                            break;
                        //아니요
                        case 2:
                            Managers.Scene.LoadScene(Define.Scene.TitleScene);
                            break;
                        default:
                            break;
                }
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (pauseGame == true && reallySelect == true&& PlayerController.noLifeCheck == false)
                {
                    ReallyActive(false);
                }
            }

        if (storyOn == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Time.timeScale = 1.0f;
            }
        }

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
    IEnumerator Stage1Play()
    {
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, -1), instPointTopLeft));
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, -1), instPointTopRight));
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(0, -1), instPointTopMidLeft));
            StartCoroutine(EnemySpawn(new Vector2(0, -1), instPointTopMidRight));
        }
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, -1), instPointTopLeft));
            StartCoroutine(EnemySpawn(new Vector2(-1, -1), instPointTopRight));
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 1; i++)
        {
            //RightMove
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft04));
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft04));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            //LeftMove
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight04));
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight04));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
        }
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(0, -1), instPointTopMidLeft));
            StartCoroutine(EnemySpawn(new Vector2(0, -1), instPointTopMidRight));
            StartCoroutine(EnemySpawn(new Vector2(1, -1), instPointTopLeft));
            StartCoroutine(EnemySpawn(new Vector2(-1, -1), instPointTopRight));
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++)
        {
            //RightMove
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft04));
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft04));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft06));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(1, 0), instPointLeft07));
            //LeftMove
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight04));
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight04));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight03));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight02));
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight01));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight06));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemySpawn(new Vector2(-1, 0), instPointRight07));
            yield return new WaitForSeconds(0.5f);

        }
        StartCoroutine(StartBeforeBossBattle());
        yield return new WaitForSeconds(1.0f);
    }


    IEnumerator EnemySpawn(Vector2 dir , Vector3 Position)
    {
        yield return new WaitForSeconds(0.1f);
        InstanceEnemy(dir, Position);
        //0.5초 대기

    }

    IEnumerator BossSpwan()
    {
        yield return new WaitForSeconds(1.0f);
        InstanceBoss(new Vector2(1,-1), bossInstPoint);
        //0.5초 대기
        yield return new WaitForSeconds(1.4f);
        storyOn = true;
    }

    private IEnumerator StartBeforeBossBattle()
    {
        StartCoroutine(BossSpwan());
        yield return new WaitForSeconds(1.4f);
        stage1Story.SetActive(true);
    }


    void InstanceEnemy(Vector2 direction,Vector3 instPoint)
    {
        GameObject go =
        Managers.Resource.Instantiate($"gameObjs/{Define.ENEMY_PREFAB_PATH}", enemyPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        go.transform.position = instPoint;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * enemySpeed;
    }

    void InstanceBoss(Vector2 direction, Vector3 instPoint)
    {
        GameObject go =
        Managers.Resource.Instantiate($"gameObjs/{Define.ENEMY_BOSS_STAGE_01}", bossPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        go.transform.position = instPoint;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bossSpeed;
    }

    public override void Clear()
    {
     
    }

    //플레이어의 공격을 생성하는 단계

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
    //Change Images Alpha, 나중에 유틸로 이동예정
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
        Time.timeScale = 1;
        pauseGame = false;
        escMenu.SetActive(false);
    }

    void RetryControlSwitch(int num)
    {
        bool _yes = false;
        bool _no = false;

        if (num == 1) { _yes = true; }

        if (num == 2) { _no = true; }

        ImageAlphaChanger(RetryYes, _yes);
        ImageAlphaChanger(RetryNo, _no);
    }
    void ReallyActive(bool select)
    {
        selectReally = 1;
        reallySelect = select;
        reallyMenu.SetActive(select);
        ReallyControlSwitch(selectReally);
    }

    void RetryControl(int number)
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
    void RetryGame()
    {
        PlayerController.life =  3;
        PlayerController.spell = 3;
        Time.timeScale = 1;
        retryMenu.SetActive(false);
        PlayerController.noLifeCheck = false;
        retryCount++;
    }
    void GameOver()
    {
        Managers.Scene.LoadScene(Define.Scene.LoadingScene);
        Time.timeScale = 1;
        retryMenu.SetActive(false);
    }
    private IEnumerator BossClear()
    {
        yield return new WaitForSeconds(5.0f);
        stageClear.SetActive(true);
    }


}
