using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerGuage : MonoBehaviour
{
    private Image PlayerPowerGauge = default;

    public const int PLAYER_POWER_MAX = 128;
    public static int PlayerPower = 50;
    public float guageAmount = default;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPowerGauge = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        guageAmount = PlayerPower / (float)PLAYER_POWER_MAX;
        PlayerPowerGauge.fillAmount = guageAmount;
    }
}
