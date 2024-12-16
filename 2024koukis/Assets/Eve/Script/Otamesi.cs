using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otamesi : MonoBehaviour
{
    public List<SettingScript> settingScripts = new List<SettingScript>();

    public WindowTitle windowTitle;

    public List<GameObject> windowObj = new List<GameObject>();             //各ウィンドウのリスト
    public List<Vector3> buttonsPos = new List<Vector3>();                  //ボタンリストに追加されたボタンのPosリスト
    private List<float> widths = new List<float>();


    public GameObject cursor;                                               //カーソルオブジェクト


    private RectTransform rectTransform;                                    //RectTransform
    public RectTransform[] settingButtons;
    public RectTransform[] soundButtons;


    private int selectNum;
    private int selectMax;
    private int selectMin = 0;


    private float cursorWidth;


    public enum WindowElement
    {
        None,               //何もメニューを開いていない状態
        Select,             //セレクトメニュー
        Sound,              //音量設定メニュー
        WindowSize,         //画面サイズ設定メニュー
        Device,             //デバイス変更メニュー
    }

    WindowElement windowState = WindowElement.None;


    private void Start()
    {
        for (int i = 0; i < windowObj.Count; i++)
        {
            windowObj[i].SetActive(false);
        }
        cursor.SetActive(false);


        rectTransform = cursor.GetComponent<RectTransform>();

        foreach (RectTransform uiElement in settingButtons)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }


        selectNum = 0;
        selectMax = settingButtons.Length - 1;

        cursorWidth = rectTransform.rect.width;

        if (windowObj.Count == 0)
        {
            Debug.Log("windowObjが未登録です");
        }
        if (settingButtons.Length == 0)
        {
            Debug.Log("uiElementsが未登録です");
        }

        if (settingScripts.Count == 0)
        {
            Debug.Log("SettingScriptsが未登録です");
        }
    }

    private void Update()
    {
        SelectState();
    }

    private void SwitchWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < settingButtons.Length; i++)
            {
                buttonsPos.Add(settingButtons[i].transform.localPosition);
            }
            selectMax = settingButtons.Length - 1;

            windowObj[0].SetActive(true);
            cursor.SetActive(true);

            windowState = WindowElement.Select;
        }
    }

    private void MoveCursor()
    {
        //rectTransform.anchoredPosition += new Vector2(1, 0);
        if (Input.GetKeyDown(KeyCode.UpArrow) && selectNum > selectMin)
        {
            selectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectNum < selectMax)
        {
            selectNum++;
        }

        cursor.transform.localPosition = new Vector3
            (buttonsPos[selectNum].x + (widths[selectNum] / 2) + (cursorWidth / 2),
            buttonsPos[selectNum].y,
            buttonsPos[selectNum].z);
    }

    private void SelectMenu()
    {
        MoveCursor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonsPos.Clear();

            windowObj[0].SetActive(false);
            cursor.SetActive(false);

            windowState = WindowElement.None;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttonsPos.Clear();
            switch (selectNum)
            {
                case 0:
                    settingScripts[0].backButton.OnClickButton();
                    windowState = WindowElement.None;
                    break;

                case 1:
                    settingScripts[0].soundButton.OnClickButton();
                    //for (int i = 0; i < soundButtons.Length; i++)
                    //{
                    //    buttonsPos.Add(soundButtons[i].transform.localPosition);
                    //}
                    //selectMax = soundButtons.Length - 1;
                    windowState = WindowElement.Sound;
                    break;

                case 2:
                    settingScripts[0].windowSizeButton.OnClickButton();
                    windowState = WindowElement.WindowSize;
                    break;

                case 3:
                    settingScripts[0].endButton.OnClickButton();
                    break;
            }
        }

    }

    private void SoundMenu()
    {
        MoveCursor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[1].SetActive(false);

            windowState = WindowElement.Select;
        }
    }

    private void WinSizeMenu()
    {
        MoveCursor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[2].SetActive(false);

            windowState = WindowElement.Select;
        }
    }

    private void SelectState()
    {
        switch (windowState)
        {
            case WindowElement.None:
                SwitchWindow();
                break;
            case WindowElement.Select:
                SelectMenu();
                break;
            case WindowElement.Sound:
                SoundMenu();
                Debug.Log(buttonsPos.Count);
                break;

            case WindowElement.WindowSize:
                WinSizeMenu();
                break;

            case WindowElement.Device:

                break;
        }
    }
}
