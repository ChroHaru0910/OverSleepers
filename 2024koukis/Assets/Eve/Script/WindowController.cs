using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public List<SettingScript> settingScript = new List<SettingScript>();

    [SerializeField] private BackButton backButton;
    [SerializeField] private SoundButton soundButton;
    [SerializeField] private WindowSizeButton windowSizeButton;
    [SerializeField] private EndButton endButton;

    public List<GameObject> windowObj = new List<GameObject>();             //各ウィンドウのリスト
    public List<Vector3> buttonsPos = new List<Vector3>();                  //ボタンリストに追加されたボタンのPosリスト
    private List<float> widths = new List<float>();


    public GameObject cursor;                                               //カーソルオブジェクト


    private RectTransform rectTransform;                                    //RectTransform
    public RectTransform[] uiElements;                                     //Widthの保存用


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


        for (int i = 0; i < uiElements.Length; i++)
        {
            buttonsPos.Add(uiElements[i].transform.localPosition);
        }

        

        rectTransform = cursor.GetComponent<RectTransform>();

        foreach(RectTransform uiElement in uiElements)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }


        selectNum = 0;
        selectMax = uiElements.Length - 1;

        cursorWidth = rectTransform.rect.width;

        if (windowObj.Count == 0)
        {
            Debug.Log("windowObjが未登録です");
        }
        if (uiElements.Length == 0)
        {
            Debug.Log("uiElementsが未登録です");
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
            windowObj[0].SetActive(false);
            cursor.SetActive(false);

            windowState = WindowElement.None;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectNum)
            {
                case 0:
                    backButton.OnClickButton();
                    windowState = WindowElement.None;
                    break;

                case 1:
                    soundButton.OnClickButton();
                    windowState = WindowElement.Select;
                    break;

                case 2:
                    windowSizeButton.OnClickButton();
                    windowState = WindowElement.WindowSize;
                    break;

                case 3:
                    endButton.OnClickButton();
                    break;
            }
        }
        
    }

    private void SoundMenu()
    {

    }

    private void SelectState()
    {
        switch(windowState)
        {
            case WindowElement.None:
                SwitchWindow();
                break;
            case WindowElement.Select:
                SelectMenu();
                break;
            case WindowElement.Sound:
                SoundMenu();
                break;

            case WindowElement.WindowSize:

                break;

            case WindowElement.Device:

                break;
        }
    }

}
