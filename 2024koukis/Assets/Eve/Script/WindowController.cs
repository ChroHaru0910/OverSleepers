using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    //�ʃX�N���v�g�����p����ׂ̕ϐ�
    [SerializeField] MoveCursor moveCursor;
    [SerializeField] WindowTitle windowTitle;
    [SerializeField] WindowSelect windowSelect;
    [SerializeField] WindowSound windowSound;


    //���ꂼ��̃E�B���h�E���̂�ۑ����郊�X�g
    public List<GameObject> windowObj = new List<GameObject>();

    //�J�[�\���I�u�W�F�N�g
    [SerializeField] public GameObject cursor;

    /// <summary>
    /// selectNum :���X�N���v�g�̏����ɂ���ĕϓ�����B���̐��ɂ���ăJ�[�\���ʒu�����܂�
    /// selectMin :�J�[�\���𓮂����ׂ̏��������Ŏg���B�ϓ����邱�Ƃ͂Ȃ�
    /// selectMax :�{�^���̍ő�l-1�̐����ݒ肳��Ă���B�E�B���h�E���Ƀ{�^���̐����ϓ�����B
    /// </summary>
    [HideInInspector]
    public int selectNum;
    [HideInInspector]
    public int selectMin = 0;
    [HideInInspector]
    public int selectMax;
    /// <summary>
    /// �E�B���h�E�̃��[�h���Ǘ����邽�߂̃��m
    /// Title :�^�C�g�����       Select :�ݒ���        Sound :���ʐݒ���       WindowSize :��ʃT�C�Y�̐ݒ���
    /// </summary>
    public enum WindowModeElement
    {
        Title,
        Select,
        Sound,
        WindowSize,
    }

    //WindowModeElement�̗v�f������B���̕ϐ��ɓ����Ă���v�f�ɂ���ĉ�ʂ��ς��B
    [HideInInspector]
    public WindowModeElement windowState;

    //windowState�̕ύX���Ɏg�p����B
    //�f�B���C��݂��������Ɏg��LockStateCoroutine�ŗp�����Ă���
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
