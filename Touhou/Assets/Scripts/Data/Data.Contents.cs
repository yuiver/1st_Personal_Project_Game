using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//어떻게 불러 읽을까 부분

#region Stat
[Serializable] //메모리에서 들고있는걸 파일로 변환할수 있다고 붙여줘야함
public class Stat // = 데이터 시트의 고유 아이디인데 List로 만들면 효율이 떨어짐
{
    //[SerializeField] private로 사용하고 싶다면 바꾸고 싶다면 int level 읽을수 있음
    //Json 파일의 리스트 이름과 변수명은 무조건 동일해야함, 변수의 자료형도 맞춰줘야함!
    //문제가 있다면 프로그램을 켜자마자 뒤진다.
    public int level;
    public int hp;
    public int attack;
    
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
        {
            dict.Add(stat.level, stat);
        }
        return dict;
    }
}

#endregion
