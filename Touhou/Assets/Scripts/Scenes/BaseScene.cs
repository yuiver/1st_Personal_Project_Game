using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    void Awake()
    {
        Init();
    }
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {
        
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        Object fpsController = GameObject.FindObjectOfType(typeof(CountFpsController));
        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem"; 
        }
        if (fpsController == null)
        {
            Managers.Resource.Instantiate("FpsCounter").name = "@FpsCounter";
        }

        Time.timeScale = 1.0f;
    
    }


    public abstract void Clear();
}
