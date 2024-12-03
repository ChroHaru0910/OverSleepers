using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public List<GameObject> windowObj = new List<GameObject>();             //各ウィンドウのリスト
    public List<GameObject> settingButtons = new List<GameObject>();        //設定画面に表示されるボタンのリスト
    public List<Vector3> buttonsPos = new List<Vector3>();                  //ボタンリストに追加されたボタンのPosリスト
    private List<float> widths = new List<float>();


    public GameObject cursor;                                               //カーソルオブジェクト


    private RectTransform rectTransform;                                    //RectTransform
    private RectTransform[] uiElements;                                     //Widthの保存用


    private int selectNum;
    private int selectMax;
    private int selectMin = 0;


    private float cursorWidth;

    private void Start()
    {
        for (int i = 0; i < windowObj.Count; i++)
        {
            windowObj[i].SetActive(false);
        }
        cursor.SetActive(false);

        if (windowObj.Count == 0)
        {
            Debug.Log("windowObjが未登録です");
        }
        if (settingButtons.Count == 0)
        {
            Debug.Log("settingObjが未登録です");
        }



        for (int i = 0; i < settingButtons.Count; i++)
        {
            buttonsPos.Add(settingButtons[i].transform.localPosition);
        }

        

        rectTransform = cursor.GetComponent<RectTransform>();

        foreach(RectTransform uiElement in uiElements)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }


        selectNum = 0;
        selectMax = settingButtons.Count - 1;

        cursorWidth = rectTransform.rect.width;
    }

    private void Update()
    {
        SwitchWindow();
        if(windowObj[0].activeSelf==true)
        {
            MoveCursor();
        }

    }

    private void SwitchWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[0].SetActive(!windowObj[0].activeSelf);
            cursor.SetActive(!cursor.activeSelf);
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

        cursor.transform.localPosition = buttonsPos[selectNum];
    }

}
