using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    //別スクリプトを引用する為の変数
    public WindowController windowController;

    //カーソルオブジェクト
    [SerializeField] GameObject cursor;

    //selectButton, soundButton, windowSizeButtonのいずれかがこのリストに追加される
    [HideInInspector]
    public List<Vector3> buttonPos = new List<Vector3>();
    //WindowControllerのwindowStateによってリスト内容が異なる。
    //各ウィンドウに存在するボタンの横幅(x軸方向の長さ)を取得し、リストに追加する。
    [HideInInspector]
    public List<float> widths = new List<float>();
    //カーソルオブジェクトの横幅(x軸方向の長さ)を取得
    private float cursorWidth;


    //カーソルの横幅を取得するときに使う。
    private RectTransform cursorRect;

    //各ウィンドウのボタンがそれぞれ保存されたリスト。
    //Unity上での設定が必要
    public RectTransform[] selectButtons;
    public RectTransform[] soundButtons;

    //[SerializeField] GameObject cursor;
    void Start()
    {
        cursorRect = cursor.GetComponent<RectTransform>();
        cursorWidth = cursorRect.rect.width;

        for (int i = 0; i < selectButtons.Length; i++)
        {
            buttonPos.Add(selectButtons[i].transform.localPosition);
        }
        foreach (RectTransform uiElements in selectButtons)
        {
            float width = uiElements.rect.width;
            widths.Add(width);
        }

    }

    public void Move()
    {
        int i = 0;
        i = windowController.getsetSelectNum;
        cursor.transform.localPosition = new Vector3
            (buttonPos[i].x + (widths[i] / 2) + (cursorWidth / 2),
            buttonPos[i].y,
            buttonPos[i].z);
    }
    /// <summary>
    /// 画面上のボタン数の再カウント(セレクト画面用)
    /// </summary>
    public void ResetSelectButtonPosition()
    {
        Debug.Log(buttonPos.Count + "  " + widths.Count);
        buttonPos.Clear();
        widths.Clear();

        for (int i = 0; i < selectButtons.Length; i++)
        {
            buttonPos.Add(selectButtons[i].transform.localPosition);
            widths.Add(selectButtons[i].rect.width);
        }
    }
    /// <summary>
    /// 画面上のボタン数の再カウント(サウンド設定画面用)
    /// </summary>
    public void ResetSoundButtonPosition()
    {
        buttonPos.Clear();
        widths.Clear();

        for (int i = 0; i < soundButtons.Length; i++)
        {
            buttonPos.Add(soundButtons[i].transform.localPosition);
        }
        foreach (RectTransform uiElements in soundButtons)
        {
            float width = uiElements.rect.width;
            widths.Add(width);
        }
    }
}
