using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base

{
    enum GameObjects
    { 
        ItemIcon,
        ItemNameText,
    }

    string _name;
    void Start()
    {
        Init();
    } 

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => {  Util.Log($"아이템 클릭! {_name}"); });

    }

    public void SetInfo(string name)
    { 
        _name= name;
    }
}
