using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonTest : MonoBehaviour
{
    GameObject onObj = default;
    GameObject offObj = default;

    // Start is called before the first frame update
    void Start()
    {
        offObj = gameObject.transform.GetChild(0).gameObject;
        onObj = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckOnOff(bool isOn)
    {
        onObj.SetActive(isOn);
    }
}
