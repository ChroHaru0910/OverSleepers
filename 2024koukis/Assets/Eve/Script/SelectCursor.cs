using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour
{
    RectTransform rectPos;                              //RectTransformの取得用
    private Vector3 backPos = new Vector3(-540, 400, 0);        //戻るボタンのPosition
    private Vector3 audioPos = new Vector3(330, 170, 0);        //サウンド設定ボタンのPosition
    private Vector3 windowPos = new Vector3(330, -50, 0);       //画面サイズ設定ボタンのPosition
    private Vector3 endPos = new Vector3(330, -270, 0);         //終了ボタンのPosition
    private Vector3[] selectPos;                        //動かすカーソルのPositionの保存用配列

    private int selectNum;                              //selectPosの要素番号

    EndButton end;


    // Start is called before the first frame update
    void Start()
    {
        rectPos = GetComponent<RectTransform>();
        selectPos = new Vector3[] { backPos, audioPos, windowPos, endPos };
        selectNum = 0;

        end = GetComponent<EndButton>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();               //カーソルの移動

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter");
            switch (selectNum)
            {
                case 0:
                    Debug.Log("back");
                    break;

                case 1:
                    Debug.Log("audio");
                    break;

                case 2:
                    Debug.Log("window");
                    break;

                case 3:
                    end.button.onClick.AddListener(end.SwitchObj);
                    break;
            }
        }
        
    }

    void MoveCursor()
    {
        rectPos.localPosition = selectPos[selectNum];
        if (Input.GetKeyDown(KeyCode.DownArrow) && selectNum < selectPos.Length - 1)
        {
            selectNum++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && selectNum > 0)
        {
            selectNum--;
        }
    }
}
