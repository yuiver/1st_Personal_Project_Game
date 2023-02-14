using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//  JSON 과 XML 두가지가 있는데 JSON은 빠르고 작성이 간결하다는 장점이 있고  
//  표현력은 XML이 위, 속도는 JSON이 위 최신프로젝트는 보통 JSON으로 가는데 원본데이터는  XML로 디자이너들이 엑셀로 관리하는데 엑셀 파일을 JSON으로 바꿔치기하는 툴을 사용해서 서버와 클라에서는 JSON을 사용
//  XML의 장점은 데이터가 복잡해진다는 가정에서 계층구조를 쉽게 파악 가능하다는 장점이 있다. 데이터 시트가 파고들어가서 5~6층 이상의 계층 구조를 가지면 JSON으로는 파악하기 매우 힘들다

// 파일을 불러 읽어오는 부분

// 스코어 저장을 JSON파일로 하도록 하자
public interface ILoader<key, Value>
{
    Dictionary<key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
        Debug.Log(StatDict[1]);
    }

    Loader LoadJson<Loader, key, Value>(string path) where Loader : ILoader<key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");  //경로 설정
        return JsonUtility.FromJson<Loader>(textAsset.text); //Json 파일로 부터 불러온다.
        //data.stats.ToDictionary(); 의 문제는 IOS쪽에서 Linq가 굉장히 문제가 많아서 사용하지 않는편이라고 한다


        

    }
}
