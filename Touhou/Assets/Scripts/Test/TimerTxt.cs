using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTxt : MonoBehaviour
{
    int CurrentTimeSecond = 0;

    IEnumerator updateTimer()
    {
        while (true)
        {
            // { 시간을 UI에 업데이트 하는 로직
            string timeStrFormat = "{0:D2} : {1:D2}";
            string timeStr = string.Format(timeStrFormat, Mathf.FloorToInt(CurrentTimeSecond / 60.0f), Mathf.FloorToInt(CurrentTimeSecond % 60.0f));
            gameObject.SetTmpText(timeStr);
            // } 시간을 UI에 업데이트 하는 로직
            // { 시간이 1초씩 흘러가는 로직
            yield return new WaitForSeconds(1.0f);
            CurrentTimeSecond++;
            // } 시간이 1초씩 흘러가는 로직
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("updateTimer");
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("updateTimer");
    }
}
