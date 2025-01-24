using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    //�ʃX�N���v�g�����p����ׂ̕ϐ�
    public WindowController windowController;

    //�J�[�\���I�u�W�F�N�g
    [SerializeField] GameObject cursor;

    //selectButton, soundButton, windowSizeButton�̂����ꂩ�����̃��X�g�ɒǉ������
    [HideInInspector]
    public List<Vector3> buttonPos = new List<Vector3>();
    //WindowController��windowState�ɂ���ă��X�g���e���قȂ�B
    //�e�E�B���h�E�ɑ��݂���{�^���̉���(x�������̒���)���擾���A���X�g�ɒǉ�����B
    [HideInInspector]
    public List<float> widths = new List<float>();
    //�J�[�\���I�u�W�F�N�g�̉���(x�������̒���)���擾
    private float cursorWidth;


    //�J�[�\���̉������擾����Ƃ��Ɏg���B
    private RectTransform cursorRect;

    //�e�E�B���h�E�̃{�^�������ꂼ��ۑ����ꂽ���X�g�B
    //Unity��ł̐ݒ肪�K�v
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
    /// ��ʏ�̃{�^�����̍ăJ�E���g(�Z���N�g��ʗp)
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
    /// ��ʏ�̃{�^�����̍ăJ�E���g(�T�E���h�ݒ��ʗp)
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
