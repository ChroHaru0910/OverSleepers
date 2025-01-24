using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSound : MonoBehaviour
{
    //別スクリプトを引用する為の変数
    public WindowController windowController;
    public SoundScripts soundScripts;
    public MoveCursor moveCursor;

    //ウィンドウのアクティブ切り替えをする時に使う
    [SerializeField] GameObject windowSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SoundOption()
    {
        //if (windowSound.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        //{
        //    windowSound.SetActive(false);
        //    windowController.windowState = WindowController.WindowModeElement.Select;
        //    windowController.LockStateForFrames(10);
        //}

        //処理内容：WindowSelect.csに同じ
        if (Input.GetKeyDown(KeyCode.UpArrow) && windowController.selectNum > windowController.selectMin)
        {
            windowController.getsetSelectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && windowController.selectNum < windowController.selectMax)
        {
            windowController.getsetSelectNum++;
        }

        if (Input.GetKeyDown(KeyCode.Return) && windowController.windowState == WindowController.WindowModeElement.Sound)
        {
            switch(windowController.selectNum)
            {
                //windowStateがSelectに変更され、各ウィンドウのアクティブ状態が切り替わる
                case 0:
                    soundScripts.geneButton.ToggleActive();
                    windowController.windowState = WindowController.WindowModeElement.Select;
                    windowController.LockStateForFrames_Select(10);
                    break;

                //BGMの音量が+5%される
                case 1:
                    soundScripts.bgmText.IncButton();
                    break;

                //BGMの音量が-5%される
                case 2:
                    soundScripts.bgmText.DecButton();
                    break;

                //SEの音量が+5%される
                case 3:
                    soundScripts.seText.IncButton();
                    break;

                //SEの音量が-5%される
                case 4:
                    soundScripts.seText.DecButton();
                    break;
            }
        }


    }
    
}
