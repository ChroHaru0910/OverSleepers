using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSelect : MonoBehaviour
{
    //�ʃX�N���v�g�����p����ׂ̕ϐ�
    public WindowController windowController;
    public SettingScript settingScript;
    public MoveCursor moveCursor;

    //�E�B���h�E�̃A�N�e�B�u�؂�ւ������鎞�Ɏg��
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
            windowController.windowState = WindowController.WindowModeElement.Title;        //�X�e�[�g�̕ύX
            windowController.LockStateForFrames_Select(10);     //�f�B���C
        }

        //�J�[�\���𓮂����ׂ�selectNum��ύX�B���L�[�ŉ��ɁA��L�[�ŏ�ɃJ�[�\�����ړ��B
        if (Input.GetKeyDown(KeyCode.UpArrow) && windowController.selectNum > windowController.selectMin)
        {
            windowController.getsetSelectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && windowController.selectNum < windowController.selectMax)
        {
            windowController.getsetSelectNum++;
        }

        //�J�[�\���̈ʒu�ɂ���ď������قȂ�BEnter�L�[�Ŋe���������s
        if (Input.GetKeyDown(KeyCode.Return)&&windowController.windowState==WindowController.WindowModeElement.Select)
        {
            switch (windowController.selectNum)
            {
                //windowState��Title�ɕύX����A�e�E�B���h�E�̃A�N�e�B�u��Ԃ��؂�ւ��
                case 0:
                    settingScript.backButton.OnClickButton();
                    windowController.selectNum = 0;
                    windowController.windowState = WindowController.WindowModeElement.Title;
                    windowController.LockStateForFrames_Select(10);
                    break;

                //windowState��Sound�ɕύX����A�e�E�B���h�E�̃A�N�e�B�u��Ԃ��؂�ւ��
                case 1:
                    settingScript.soundButton.OnClickButton();
                    windowController.selectMax = moveCursor.soundButtons.Length - 1;
                    windowController.windowState = WindowController.WindowModeElement.Sound;
                    windowController.LockStateForFrames_Sound(10);
                    break;

                //windowState��WindowSize�ɕύX����A�e�E�B���h�E�̃A�N�e�B�u��Ԃ��؂�ւ��
                case 2:
                    settingScript.windowSizeButton.OnClickButton();
                    break;

                //windowState�̕ύX�͖����B�Q�[���̋����I�����������s
                case 3:
                    settingScript.endButton.OnClickButton();
                    break;
            }
        }
    }
}
