using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class Util
{
    public static void SetTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();
        if (tmpTxt == null || tmpTxt == default(TMP_Text))
        {
            return;
        }
        tmpTxt.text = text_;
    }       // SetTextMeshPro()
}
