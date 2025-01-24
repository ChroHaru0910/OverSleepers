using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSelect : MonoBehaviour
{
    //別スクリプトを引用する為の変数
    public WindowController windowController;
    public SettingScript settingScript;
    public MoveCursor moveCursor;

    //ウィンドウのアクティブ切り替えをする時に使う
    [SerializeField] GameObject windowSetting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectOption()
    {
        if (windowSetting.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            windowSetting.SetActive(false);
            windowController.cursor.SetActive(false);
            windowController.windowState = WindowController.WindowModeElement.Title;        //ステートの変更
            windowController.LockStateForFrames_Select(10);     //ディレイ
        }

        //カーソルを動かす為にselectNumを変更。下キーで下に、上キーで上にカーソルが移動。
        if (Input.GetKeyDown(KeyCode.UpArrow) && windowController.selectNum > windowController.selectMin)
        {
            windowController.getsetSelectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && windowController.selectNum < windowController.selectMax)
        {
            windowController.getsetSelectNum++;
        }

        //カーソルの位置によって処理が異なる。Enterキーで各処理を実行
        if (Input.GetKeyDown(KeyCode.Return)&&windowController.windowState==WindowController.WindowModeElement.Select)
        {
            switch (windowController.selectNum)
            {
                //windowStateがTitleに変更され、各ウィンドウのアクティブ状態が切り替わる
                case 0:
                    settingScript.backButton.OnClickButton();
                    windowController.selectNum = 0;
                    windowController.windowState = WindowController.WindowModeElement.Title;
                    windowController.LockStateForFrames_Select(10);
                    break;

                //windowStateがSoundに変更され、各ウィンドウのアクティブ状態が切り替わる
                case 1:
                    settingScript.soundButton.OnClickButton();
                    windowController.selectMax = moveCursor.soundButtons.Length - 1;
                    windowController.windowState = WindowController.WindowModeElement.Sound;
                    windowController.LockStateForFrames_Sound(10);
                    break;

                //windowStateがWindowSizeに変更され、各ウィンドウのアクティブ状態が切り替わる
                case 2:
                    settingScript.windowSizeButton.OnClickButton();
                    break;

                //windowStateの変更は無い。ゲームの強制終了処理を実行
                case 3:
                    settingScript.endButton.OnClickButton();
                    break;
            }
        }
    }
}
