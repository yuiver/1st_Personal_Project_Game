using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image playerLifeGauge;
    public Image playerSpellGauge;
    public Image playerPowerGauge;

    public TMP_Text highScoreText;
    public TMP_Text scoreText;
    public TMP_Text powerText;
    public TMP_Text grazeText;
    public TMP_Text pointItemText;
    //이새끼는 진짜 뭔지 모름 하수인 죽일때 나오는 분홍색 무언가로 체크하는거 같은데 잘 모르겠다.
    public TMP_Text timeText;
    public TMP_Text FPS_Text;

    public UnityEvent onHighScoreChanged;
    public UnityEvent onScoreChanged;
    public UnityEvent onPowerChanged;
    public UnityEvent onGrazeChanged;
    public UnityEvent onPointItemChanged;
    public UnityEvent onTimeChanged;


    private int life;
    private int spell;

    public static int power;

    private int highScore;
    private int score;
    private int graze;
    private int pointItem;
    private int time;
    private float FPS;


    private const byte SPELL_MAX = 8;
    private const byte LIFE_MAX = 8;
    private const int POWER_MAX = 128;
    public float powerGuageAmount = default;
    public float lifeGuageAmount = default;
    public float spellGuageAmount = default;

    //이 스타트에서 게임플레이가 시작하는 순간에 초기값을 입력할수있다.
    void Start()
    {
        // 초기값 설정
        life = 3;
        spell = 3;
        score = 0;
        power = 0;
        graze = 0;
        time = 0;
        // UI 초기화
        UpdateHighScoreText();
        UpdateScoreText();
        UpdatePowerText();
        UpdateGrazeText();
        UpdatePointItemText();
        UpdateTimeText();
        playerLifeGauge.GetComponent<Image>();
        playerPowerGauge.GetComponent<Image>();
        playerSpellGauge.GetComponent<Image>();
    }
    private void Update()
    {
        lifeGuageAmount = life / (float)LIFE_MAX;
        playerLifeGauge.fillAmount = lifeGuageAmount;

        spellGuageAmount = spell / (float)SPELL_MAX;
        playerSpellGauge.fillAmount = spellGuageAmount;

        powerGuageAmount = power / (float)POWER_MAX;
        playerPowerGauge.fillAmount = powerGuageAmount;
    }

    // 변수의 값을 변경하고 이벤트를 발생시킵니다.
    public void SetHighScore(int value)
    {
        life = value;
        onHighScoreChanged.Invoke();
    }
    public void SetScore(int value)
    {
        score = value;
        onScoreChanged.Invoke();
    }
    public void SetPower(int value)
    {
        spell = value;
        onPowerChanged.Invoke();
    }
    public void SetGraze(int value)
    {
        life = value;
        onGrazeChanged.Invoke();
    }
    public void SetPointItem(int value)
    {
        life = value;
        onPointItemChanged.Invoke();
    }
    public void SetTime(int value)
    {
        life = value;
        onTimeChanged.Invoke();
    }

    // UI를 업데이트하는 메서드
    private void UpdateHighScoreText()
    {
        scoreText.text = string.Format("{0:D10}", highScore);
    }
    private void UpdateScoreText()
    {
        
        scoreText.text = string.Format("{0:D10}",score);
    }
    private void UpdatePowerText()
    {
        powerText.text = power.ToString();
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



    // 이벤트 리스너 메서드
    private void OnHighScoreChangedHandler()
    {
        UpdateHighScoreText();
    }
    private void OnScoreChangedHandler()
    {
        UpdateScoreText();
    }
    private void OnGrazeChangedHandler()
    {
        UpdateGrazeText();
    }
    private void OnPowerChangedHandler()
    {
        UpdatePowerText();
    }
    private void OnPointItemChangedHandler()
    {
        UpdatePointItemText();
    }
    private void OnTimeChangedHandler()
    {
        UpdateTimeText();
    }


    // 이벤트 리스너 등록 및 해제
    private void OnEnable()
    {
        onScoreChanged.AddListener(OnScoreChangedHandler);
        onHighScoreChanged.AddListener(OnHighScoreChangedHandler);
        onPowerChanged.AddListener(OnPowerChangedHandler);
        onGrazeChanged.AddListener(OnGrazeChangedHandler);
        onPointItemChanged.AddListener(OnPointItemChangedHandler);

    }

    private void OnDisable()
    {
        onHighScoreChanged.RemoveListener(OnHighScoreChangedHandler);
        onScoreChanged.RemoveListener(OnScoreChangedHandler);
        onPowerChanged.RemoveListener(OnPowerChangedHandler);
        onGrazeChanged.RemoveListener(OnGrazeChangedHandler);
        onPointItemChanged.RemoveListener(OnPointItemChangedHandler);
    }
}
