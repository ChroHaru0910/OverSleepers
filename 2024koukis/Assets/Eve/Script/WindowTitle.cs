using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowTitle : MonoBehaviour
{
    //�ʃX�N���v�g�����p����ׂ̕ϐ�
    public WindowController windowController;
    public MoveCursor moveCursor;

    //�E�B���h�E�̃A�N�e�B�u�؂�ւ������鎞�Ɏg��
    [SerializeField] GameObject windowSetting;


    private void Start()
    {

    }

    public void OpenMenu()
    {
        if(!windowSetting.activeSelf)
        {
            //Escape�L�[�����������A�ݒ���(State��Select)���J��
            if (Input.GetKey(KeyCode.Escape))
            {
                windowController.windowState = WindowController.WindowModeElement.Select;       //�X�e�[�g�̕ύX
                windowSetting.SetActive(true);
                windowController.cursor.SetActive(true);
                windowController.LockStateForFrames_Select(10);     //10�t���[���̃f�B���C
            }
            else if (Input.anyKey)
            {
                //Escape�L�[�ȊO�����������A�V�[���J��
                SceneManager.LoadScene("TestGameScene");
            }
        }
    }
}
