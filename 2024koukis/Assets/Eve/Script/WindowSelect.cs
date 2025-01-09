using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSelect : MonoBehaviour
{
    public WindowController windowController;
    public SettingScript settingScript;
    public MoveCursor moveCursor;

    [SerializeField] GameObject windowSetting;
    //[SerializeField] GameObject cursor;

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
            windowController.windowState = WindowController.WindowModeElement.None;
            windowController.LockStateForFrames(10);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && windowController.selectNum > windowController.selectMin)
        {
            windowController.selectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && windowController.selectNum < windowController.selectMax)
        {
            windowController.selectNum++;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (windowController.selectNum)
            {
                case 0:
                    windowController.windowState = WindowController.WindowModeElement.None;
                    settingScript.backButton.OnClickButton();
                    windowController.LockStateForFrames(10);
                    break;

                case 1:
                    windowController.windowState = WindowController.WindowModeElement.Sound;
                    settingScript.soundButton.OnClickButton();
                    moveCursor.ConvertToSounds();
                    windowController.LockStateForFrames(10);
                    break;

                case 2:
                    settingScript.windowSizeButton.OnClickButton();
                    break;

                case 3:
                    settingScript.endButton.OnClickButton();
                    break;
            }
        }
    }
}
