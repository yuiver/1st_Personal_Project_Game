using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItest : MonoBehaviour
{
    public List<GameObject> uiList;
    private int selectedIndex = 0;
    private GameObject selectedUI;
    private Canvas canvas;

    void Start()
    {
        // Canvas를 찾습니다.
        canvas = GetComponent<Canvas>();
        // 처음에 첫 번째 UI를 선택합니다.
        SelectUI(selectedIndex);
    }

    void Update()
    {
        // 방향키 입력을 받아 UI 선택을 변경합니다.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = uiList.Count - 1;
            SelectUI(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= uiList.Count) selectedIndex = 0;
            SelectUI(selectedIndex);
        }
        // 엔터키 입력을 받아 선택된 UI를 활성화합니다.
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedUI != null)
            {
                selectedUI.SetActive(false);
            }
            selectedUI = uiList[selectedIndex];
            selectedUI.SetActive(true);
        }
    }

    // 선택된 UI를 저장하고 강조합니다.
    private void SelectUI(int index)
    {
        // 이전 선택된 UI를 비활성화합니다.
        if (selectedUI != null)
        {
            selectedUI.SetActive(false);
        }
        // 선택된 UI를 저장합니다.
        selectedIndex = index;
        selectedUI = uiList[selectedIndex];
        // 선택된 UI를 강조합니다.
        selectedUI.SetActive(true);
    }

    // UI를 전체적으로 활성화 또는 비활성화합니다.
    public void SetUIActive(bool isActive)
    {
        canvas.gameObject.SetActive(isActive);
    }
}
