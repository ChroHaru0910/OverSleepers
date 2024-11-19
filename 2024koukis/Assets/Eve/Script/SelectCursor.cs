using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour
{
    [SerializeField] GameObject backObj;
    [SerializeField] GameObject soundObj;

    RectTransform rectPos;                              //RectTransform�̎擾�p
    private Vector3 backPos = new Vector3(-540, 400, 0);        //�߂�{�^����Position
    private Vector3 audioPos = new Vector3(330, 170, 0);        //�T�E���h�ݒ�{�^����Position
    private Vector3 windowPos = new Vector3(330, -50, 0);       //��ʃT�C�Y�ݒ�{�^����Position
    private Vector3 endPos = new Vector3(330, -270, 0);         //�I���{�^����Position
    private Vector3[] selectPos;                        //�������J�[�\����Position�̕ۑ��p�z��

    private int selectNum;                              //selectPos�̗v�f�ԍ�

    EndButton end;

    enum Device
    {
        KeyBoard,
        Controller,
    }

    // Start is called before the first frame update
    void Start()
    {
        rectPos = GetComponent<RectTransform>();
        selectPos = new Vector3[] { backPos, audioPos, windowPos, endPos };
        selectNum = 0;

        end = GetComponent<EndButton>();


        string[] connectedJoysticks = Input.GetJoystickNames();
        foreach (string joystick in connectedJoysticks)
        {
            Debug.Log("Connected Joystick: " + joystick);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float L_Stick_H = Input.GetAxis("L_Stick_H");
        float L_Stick_V = Input.GetAxis("L_Stick_V");

        if(L_Stick_H!=0||L_Stick_V!=0)
        {
            Debug.Log("stick:" + L_Stick_H + "," + L_Stick_V);
        }

        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("aaa");
        }


        for (int i = 0; i < 20; i++) // �ő�20�̃{�^�����m�F�i�����\�j
        {
            if (Input.GetButtonDown("joystick button 0" + i))
            {
                Debug.Log("Joystick Button " + i + " Pressed");
            }
        }


        MoveCursor();               //�J�[�\���̈ړ�

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter");
            switch (selectNum)
            {
                case 0:
                    backObj.SetActive(false);
                    Debug.Log("back");
                    break;

                case 1:
                    soundObj.SetActive(true);
                    Debug.Log("audio");
                    break;

                case 2:
                    Debug.Log("window");
                    break;

                case 3:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
                    Debug.Log("end");
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
