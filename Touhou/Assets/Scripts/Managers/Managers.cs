using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다.
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다.


    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();


    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    
    }


    // DontDestroyOnLoad로 로드되는 매니저에 많은 데이터가 쌓이게 되므로 캐싱하게 된다면 메모리 누수가 생기기 때문에 Clear함수를 만들어서 메모리를 관리한다.
    // 이 함수는 SceneManagerEx에서 씬이 넘어가는 경우에 호출하게 된다.
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
