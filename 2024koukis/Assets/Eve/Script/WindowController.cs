using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    //別スクリプトを引用する為の変数
    [SerializeField] MoveCursor moveCursor;
    [SerializeField] WindowTitle windowTitle;
    [SerializeField] WindowSelect windowSelect;
    [SerializeField] WindowSound windowSound;


    //それぞれのウィンドウ自体を保存するリスト
    public List<GameObject> windowObj = new List<GameObject>();

    //カーソルオブジェクト
    [SerializeField] public GameObject cursor;

    /// <summary>
    /// selectNum :他スクリプトの処理によって変動する。この数によってカーソル位置が決まる
    /// selectMin :カーソルを動かす為の条件処理で使う。変動することはない
    /// selectMax :ボタンの最大値-1の数が設定されている。ウィンドウ毎にボタンの数が変動する。
    /// </summary>
    [HideInInspector]
    public int selectNum;
    [HideInInspector]
    public int selectMin = 0;
    [HideInInspector]
    public int selectMax;
    /// <summary>
    /// ウィンドウのモードを管理するためのモノ
    /// Title :タイトル画面       Select :設定画面        Sound :音量設定画面       WindowSize :画面サイズの設定画面
    /// </summary>
    public enum WindowModeElement
    {
        Title,
        Select,
        Sound,
        WindowSize,
    }

    //WindowModeElementの要素が入る。この変数に入っている要素によって画面が変わる。
    [HideInInspector]
    public WindowModeElement windowState;

    //windowStateの変更時に使用する。
    //ディレイを設けたい時に使うLockStateCoroutineで用いられている
    private bool stateLock = false;

    private void Start()
    {
        windowState = WindowModeElement.Title;
        selectNum = 0;
        selectMax = moveCursor.selectButtons.Length - 1;
    }

    public int getsetSelectNum
    {
        get { return selectNum; }
        set { selectNum = value; }
    }

    public void Update()
    {
        if (stateLock) return;

        switch (windowState)
        {
            case WindowModeElement.Title:
                Debug.Log("None");
                windowTitle.OpenMenu();
                break;

            case WindowModeElement.Select:
                Debug.Log("Select");
                windowSelect.SelectOption();
                moveCursor.Move();
                break;

            case WindowModeElement.Sound:
                Debug.Log("Sound");
                windowSound.SoundOption();
                moveCursor.Move();
                
                
                break;

            case WindowModeElement.WindowSize:

                break;
        }
    }


    public void LockStateForFrames_Sound(int frames)
    {
        StartCoroutine(LockStateCoroutine(frames));
        moveCursor.ResetSoundButtonPosition();
    }

    public void LockStateForFrames_Select(int frames)
    {
        StartCoroutine(LockStateCoroutine(frames));
        moveCursor.ResetSelectButtonPosition();
    }

    public IEnumerator LockStateCoroutine(int frames)
    {
        stateLock = true;
        moveCursor.buttonPos.Clear();
        moveCursor.widths.Clear();
        for (int i = 0; i < frames; i++)
        {
            yield return null;
        }
        stateLock = false;
    }

}
