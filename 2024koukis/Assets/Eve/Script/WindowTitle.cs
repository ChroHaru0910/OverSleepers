using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowTitle : MonoBehaviour
{
    //別スクリプトを引用する為の変数
    public WindowController windowController;
    public MoveCursor moveCursor;

    //ウィンドウのアクティブ切り替えをする時に使う
    [SerializeField] GameObject windowSetting;


    private void Start()
    {

    }

    public void OpenMenu()
    {
        if(!windowSetting.activeSelf)
        {
            //Escapeキーを押した時、設定画面(StateがSelect)を開く
            if (Input.GetKey(KeyCode.Escape))
            {
                windowController.windowState = WindowController.WindowModeElement.Select;       //ステートの変更
                windowSetting.SetActive(true);
                windowController.cursor.SetActive(true);
                windowController.LockStateForFrames_Select(10);     //10フレームのディレイ
            }
            else if (Input.anyKey)
            {
                //Escapeキー以外を押した時、シーン遷移
                SceneManager.LoadScene("TestGameScene");
            }
        }
    }
}
