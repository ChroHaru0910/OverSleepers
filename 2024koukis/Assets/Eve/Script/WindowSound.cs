using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSound : MonoBehaviour
{
    //�ʃX�N���v�g�����p����ׂ̕ϐ�
    public WindowController windowController;
    public SoundScripts soundScripts;
    public MoveCursor moveCursor;

    //�E�B���h�E�̃A�N�e�B�u�؂�ւ������鎞�Ɏg��
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

        //�������e�FWindowSelect.cs�ɓ���
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
                //windowState��Select�ɕύX����A�e�E�B���h�E�̃A�N�e�B�u��Ԃ��؂�ւ��
                case 0:
                    soundScripts.geneButton.ToggleActive();
                    windowController.windowState = WindowController.WindowModeElement.Select;
                    windowController.LockStateForFrames_Select(10);
                    break;

                //BGM�̉��ʂ�+5%�����
                case 1:
                    soundScripts.bgmText.IncButton();
                    break;

                //BGM�̉��ʂ�-5%�����
                case 2:
                    soundScripts.bgmText.DecButton();
                    break;

                //SE�̉��ʂ�+5%�����
                case 3:
                    soundScripts.seText.IncButton();
                    break;

                //SE�̉��ʂ�-5%�����
                case 4:
                    soundScripts.seText.DecButton();
                    break;
            }
        }


    }
    
}
