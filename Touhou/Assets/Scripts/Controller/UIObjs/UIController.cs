using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public UnityEvent onLifechanged;
    public UnityEvent onSpellchanged;
    public UnityEvent onHighScoreChanged;
    public UnityEvent onScoreChanged;
    public UnityEvent onPowerChanged;
    public UnityEvent onGrazeChanged;
    public UnityEvent onPointItemChanged;
    public UnityEvent onTimeChanged;

    //이 스타트에서 게임플레이가 시작하는 순간에 초기값을 입력할수있다.




    IEnumerator ShootCoroutine()
    {
        while (true)
        {

            //0.5초 대기
            yield return new WaitForSeconds(0.1f);
        }
    }
}
