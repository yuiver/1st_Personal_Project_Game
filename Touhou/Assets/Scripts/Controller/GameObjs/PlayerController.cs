using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    #region Player have Status

    CircleCollider2D PlayerCollider;

    [SerializeField]
    private Image playerLifeGauge;
    [SerializeField]
    private Image playerSpellGauge;
    [SerializeField]
    private Image playerPowerGauge;

    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text powerText;
    [SerializeField]
    private TMP_Text grazeText;
    [SerializeField]
    private TMP_Text pointItemText;
    //이새끼는 진짜 뭔지 모름 하수인 죽일때 나오는 분홍색 무언가로 체크하는거 같은데 잘 모르겠다.
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text FPS_Text;


    private const int SPELL_MAX = 8;
    private const int LIFE_MAX = 8;
    private const int POWER_MAX = 128;

    public static int life = default;
    public static int spell = default;
    public static int highScore = default;
    public static int pointItem = default;
    public static int time = default;
    public static int score = default;
    public static int graze = default;

    private float powerGuageAmount = default;
    private float lifeGuageAmount = default;
    private float spellGuageAmount = default;

    private float TimeChecker = 0f;

    public static int power = default;
    public int levelnum = default;
    public Image gameLevel;
    public Sprite[] levelSprite = new Sprite[4];
    #endregion


    private int death = default;

    [SerializeField]
    private float speed = 8; //스피드 
    private float xMove, yMove;
    private float bulletSpeed = 12.0f;
    private int bulletCountPerSide = default; // 한 쪽에 발사할 탄환 수
    private int guidedBulletCountPerSide = default; // 한 쪽에 발사할 탄환 수
    private float angleBetweenBullets = 5f; // 탄환 간의 각도 차이

    private GameObject playerCharaA = default;
    private GameObject playerCharaB = default;
    private GameObject bulletPrefab = default;

    public Animator anim;
    public static Vector2 playerPosition = default;

    private Vector2 ReSpawnPoint = new Vector2(-206, -273);
    private bool isShooting = false;
    private bool isOtherChara = false;
    private bool immortalTime = false;
    private bool hitTimeDelay = false;
    public static bool noLifeCheck = false;
    //public static bool playerHitOn;

    //오브젝트를 생성해줄 부모의 위치를 잡는 객체를 캐싱한다.
    GameObject gameObjParent = default;
    //오브젝트를 생성해줄 부모의 위치 객체의 Transfrom을 캐싱한다.
    Transform parent_Tf = default;
    public Transform playerBulletPoolRoot { get; set; }
    public Transform playerGuidedBulletPoolRoot { get; set; }


    void Awake()
    {
        PlayerCollider = gameObject.GetComponent<CircleCollider2D>();
        levelnum = LevelSelectScene.level - 1;
        if (levelnum >= 0)
        {
            gameLevel.sprite = levelSprite[levelnum];
        }
        #region Reset Status
        playerLifeGauge.GetComponent<Image>();
        playerPowerGauge.GetComponent<Image>();
        playerSpellGauge.GetComponent<Image>();
        // 초기값 설정
        score = 0;

        life = 3;
        spell = 3;

        power = 128;
        graze = 0;
        pointItem = 0;
        time = 0;
        #endregion
        #region Update Status Text and Image
        UpdateLifeImage();
        UpdateSpellImage();
        UpdatePower();
        UpdateHighScoreText();
        UpdateScoreText();
        UpdateGrazeText();
        UpdatePointItemText();
        UpdateTimeText();
        #endregion

        //death = 0;


        #region make Player Bullet Obj
        //총알을 풀링하는 코드
        gameObjParent = GameObject.Find("GameObjs");
        parent_Tf = gameObjParent.gameObject.transform;
        playerPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        List<GameObject> playerBulletList = new List<GameObject>();

        playerBulletPoolRoot = new GameObject().transform;
        playerBulletPoolRoot.SetParent(parent_Tf, false);
        playerBulletPoolRoot.name = $"@{Define.PLAYER_BULLET_PREFAB_PATH}_Obj";

        List<GameObject> playerGuidedBulletList = new List<GameObject>();

        playerGuidedBulletPoolRoot = new GameObject().transform;
        playerGuidedBulletPoolRoot.SetParent(parent_Tf, false);
        playerGuidedBulletPoolRoot.name = $"@{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}_Obj";

        for (int i = 0; i < 35; i++)
        {
            playerBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}", playerBulletPoolRoot));
        }
        foreach (GameObject playerBulletObj in playerBulletList)
        {
            Managers.Resource.Destroy(playerBulletObj);
        }

        for (int i = 0; i < 35; i++)
        {
            playerGuidedBulletList.Add(Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}", playerGuidedBulletPoolRoot));
        }
        foreach (GameObject playerGuidedBulletObj in playerGuidedBulletList)
        {
            Managers.Resource.Destroy(playerGuidedBulletObj);
        }
        #endregion

    }

    void OnEnable()
    {
        //오브젝트가 활성화중이면 총알을 발사하는 코루틴을 켠다. 체크는 z키로 쏠지말지 정한다.
        StartCoroutine(ShootCoroutine());
    }
    void Update()
    {
        #region 플레이어의 파워를 체크해서 몇발의 탄환을 발사할지 결정하는 if
        if (power < 45)
        {
            bulletCountPerSide = 0;
        }
        else
        {
            bulletCountPerSide = 1;
        }
        //서브 탄환 
        if (power < 25)
        {
            guidedBulletCountPerSide = 0;
        }
        else if (power < 128)
        {
            guidedBulletCountPerSide = 1;
        }
        else
        {
            guidedBulletCountPerSide = 2;
        }
        #endregion

        #region 키를 눌러서 조작하는 방식
        //플레이어의 현재위치
        playerPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);

        xMove = 0;
        yMove = 0;

        //플레이어가 이동하는 위치
        if (Input.GetKey(KeyCode.RightArrow) && hitTimeDelay == false && gameObject.transform.localPosition.x < 172.0f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            xMove = -speed * Time.deltaTime;
            if (0 > xMove) { anim.SetBool("isMoving", true); }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && hitTimeDelay == false && gameObject.transform.localPosition.x > -550.0f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            xMove = -speed * Time.deltaTime;
            if (0 > xMove) { anim.SetBool("isMoving", true); }
        }
        if (Input.GetKey(KeyCode.UpArrow) && hitTimeDelay == false && gameObject.transform.localPosition.y < 400.0f)
        {
            yMove = speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && hitTimeDelay == false && gameObject.transform.localPosition.y > -400.0f)
        {
            yMove = -speed * Time.deltaTime;
        }
        this.transform.Translate(new Vector3(xMove, yMove, 0));

        if (xMove == 0)
        {
            anim.SetBool("isMoving", false);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Z 키를 누르면 발사 시작
        if (Input.GetKeyDown(KeyCode.Z) && hitTimeDelay == false)
        {
            isShooting = true;
        }

        // Z 키를 떼면 발사 중지
        if (Input.GetKeyUp(KeyCode.Z) && hitTimeDelay == false)
        {
            isShooting = false;
        }

        //X 키를 누르면 폭탄 발사
        if (Input.GetKeyDown(KeyCode.X) && hitTimeDelay == false)
        {
            //if(UIController.)
        }

        // 왼쪽 Shift키를 누르면 이동속도가 느려지고 캐릭터를 전환하며 피격 콜라이더를 보이게 만든다.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 0.25f;
            
        }
        // 왼쪽 Shift키를 떼면 중지
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed * 4f;
        }
        #endregion

        #region Update Status Text and Image
        UpdateLifeImage();
        UpdateSpellImage();
        UpdatePower();
        UpdateHighScoreText();
        UpdateScoreText();
        UpdateGrazeText();
        UpdatePointItemText();
        UpdateTimeText();
        #endregion
    }

    #region Status Update method
    private void UpdateLifeImage()
    {
        lifeGuageAmount = life / (float)LIFE_MAX;
        playerLifeGauge.fillAmount = lifeGuageAmount;
    }
    private void UpdateSpellImage()
    {
        spellGuageAmount = spell / (float)SPELL_MAX;
        playerSpellGauge.fillAmount = spellGuageAmount;
    }
    private void UpdatePower()
    {
        if (power == 128)
        {
            powerText.text = "MAX";
        }
        else
        {
            powerText.text = power.ToString();
        }
        powerGuageAmount = power / (float)POWER_MAX;
        playerPowerGauge.fillAmount = powerGuageAmount;
    }
    private void UpdateHighScoreText()
    {
        scoreText.text = string.Format("{0:D10}", highScore);
    }
    private void UpdateScoreText()
    {
        scoreText.text = string.Format("{0:D10}", score);
    }
    private void UpdateGrazeText()
    {
        grazeText.text = graze.ToString();
    }
    private void UpdatePointItemText()
    {
        pointItemText.text = pointItem.ToString();
    }
    private void UpdateTimeText()
    {
        timeText.text = time.ToString();
    }
    #endregion

    #region Xway Bullet
    void ShootBullets()
    {
        // 중앙에 있는 탄환 발사
        ShootBullet(Vector2.up);

        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction); 
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= bulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootBullet(direction);
        }
    }
    void ShootGuidedBullets()
    {

        // 왼쪽 측면 탄환 발사
        for (int i = 1; i <= guidedBulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets + (angleBetweenBullets * bulletCountPerSide);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootGuidedBullet(direction);
        }

        // 오른쪽 측면 탄환 발사
        for (int i = 1; i <= guidedBulletCountPerSide; i++)
        {
            float angle = i * angleBetweenBullets + (angleBetweenBullets * bulletCountPerSide);
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            Vector2 direction = rotation * Vector2.up;
            ShootGuidedBullet(direction);
        }
    }
    #endregion

    #region Shoot Player Bullet
    //벨로시티로 발사하는 탄환
    void ShootBullet(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_BULLET_PREFAB_PATH}", playerBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f,0f,90f);
        go.transform.localPosition = playerPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
    void ShootGuidedBullet(Vector2 direction)
    {
        GameObject go =
        Managers.Resource.Instantiate($"GameObjs/{Define.PLAYER_GUIDED_BULLET_PREFAB_PATH}", playerGuidedBulletPoolRoot);
        go.transform.rotation = Quaternion.identity;
        go.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        go.transform.localPosition = playerPosition;
        go.transform.localScale = Vector3.one;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && immortalTime == false || collision.gameObject.CompareTag("Enemy") && immortalTime == false)
        {
            hitTimeDelay = true;
            StartCoroutine(HitAfterDelay());
            Managers.Sound.Play("SE/se_tan00");
            //PlayerCollider.enabled = false;
            StartCoroutine(DeathChecker());
            //플레이어가 사망시 발생하게 될 무언가
        }
    }
    private IEnumerator DeathChecker()
    {
        bool lastChance = false;
        TimeChecker = 0f;
        while (lastChance == false)
        {
            yield return null;
            TimeChecker += Time.deltaTime;

            Time.timeScale = 0.1f;
            if (spell > 0 && TimeChecker < 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    spell--;
                    lastChance = true;
                    Time.timeScale = 1.0f;
                    hitTimeDelay = false;
                    break;
                }
            }
            else
            {
                if (TimeChecker > 0.1f)
                {
                    Time.timeScale = 1.0f;
                    lastChance = true;
                    StartCoroutine(DeSpwanPlayer(gameObject));
                    break;
                }
            }
            
        }
    }
    private void IsLifeLeft()
    {
        if (life > 0)
        {
            StartCoroutine(ReSpawnPlayer(gameObject));
        }
        else
        {
            noLifeCheck = true;
        }

    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (isShooting == true)
            {
                Managers.Sound.Play("SE/se_plst00");
                ShootBullets();
                ShootGuidedBullets();
            }

            //0.5초 대기
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HitAfterDelay()
    {
        //playerHitOn = true;
        immortalTime = true;
        yield return new WaitForSeconds(4.0f);
        immortalTime = false;
    }

    private IEnumerator DeSpwanPlayer(GameObject target)
    {
        for (int i = 100; i == 0; i--)
        {
            yield return new WaitForSeconds(0.01f);
            Util.ImageAlphaChange(target, 0.01f * i);
        }
        IsLifeLeft();

    }
    private IEnumerator ReSpawnPlayer(GameObject target)
    {
        life--;
        gameObject.transform.localPosition = ReSpawnPoint;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Util.ImageAlphaChange(target, 0.01f * i);
        }
        spell = 3;
        Time.timeScale = 1.0f;
        hitTimeDelay = false;
    }


}