using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public List<SettingScript> settingScript = new List<SettingScript>();

    [SerializeField] private BackButton backButton;
    [SerializeField] private SoundButton soundButton;
    [SerializeField] private WindowSizeButton windowSizeButton;
    [SerializeField] private EndButton endButton;

    public List<GameObject> windowObj = new List<GameObject>();             //�e�E�B���h�E�̃��X�g
    public List<Vector3> buttonsPos = new List<Vector3>();                  //�{�^�����X�g�ɒǉ����ꂽ�{�^����Pos���X�g
    private List<float> widths = new List<float>();


    public GameObject cursor;                                               //�J�[�\���I�u�W�F�N�g


    private RectTransform rectTransform;                                    //RectTransform
    public RectTransform[] uiElements;                                     //Width�̕ۑ��p


    private int selectNum;
    private int selectMax;
    private int selectMin = 0;


    private float cursorWidth;


    public enum WindowElement
    {
        None,               //�������j���[���J���Ă��Ȃ����
        Select,             //�Z���N�g���j���[
        Sound,              //���ʐݒ胁�j���[
        WindowSize,         //��ʃT�C�Y�ݒ胁�j���[
        Device,             //�f�o�C�X�ύX���j���[
    }

    WindowElement windowState = WindowElement.None;


    private void Start()
    {
        for (int i = 0; i < windowObj.Count; i++)
        {
            windowObj[i].SetActive(false);
        }
        cursor.SetActive(false);


        for (int i = 0; i < uiElements.Length; i++)
        {
            buttonsPos.Add(uiElements[i].transform.localPosition);
        }

        

        rectTransform = cursor.GetComponent<RectTransform>();

        foreach(RectTransform uiElement in uiElements)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }


        selectNum = 0;
        selectMax = uiElements.Length - 1;

        cursorWidth = rectTransform.rect.width;

        if (windowObj.Count == 0)
        {
            Debug.Log("windowObj�����o�^�ł�");
        }
        if (uiElements.Length == 0)
        {
            Debug.Log("uiElements�����o�^�ł�");
        }
    }

    private void Update()
    {
        SelectState();
        
    }

    private void SwitchWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[0].SetActive(true);
            cursor.SetActive(true);

            windowState = WindowElement.Select;
        }
    }

    private void MoveCursor()
    {
        //rectTransform.anchoredPosition += new Vector2(1, 0);
        if (Input.GetKeyDown(KeyCode.UpArrow) && selectNum > selectMin)
        {
            selectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectNum < selectMax)
        {
            selectNum++;
        }

        cursor.transform.localPosition = new Vector3
            (buttonsPos[selectNum].x + (widths[selectNum] / 2) + (cursorWidth / 2),
            buttonsPos[selectNum].y,
            buttonsPos[selectNum].z);
    }

    private void SelectMenu()
    {
        MoveCursor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[0].SetActive(false);
            cursor.SetActive(false);

            windowState = WindowElement.None;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectNum)
            {
                case 0:
                    backButton.OnClickButton();
                    windowState = WindowElement.None;
                    break;

                case 1:
                    soundButton.OnClickButton();
                    windowState = WindowElement.Select;
                    break;

                case 2:
                    windowSizeButton.OnClickButton();
                    windowState = WindowElement.WindowSize;
                    break;

                case 3:
                    endButton.OnClickButton();
                    break;
            }
        }
        
    }

    private void SoundMenu()
    {

    }

    private void SelectState()
    {
        switch(windowState)
        {
            case WindowElement.None:
                SwitchWindow();
                break;
            case WindowElement.Select:
                SelectMenu();
                break;
            case WindowElement.Sound:
                SoundMenu();
                break;

            case WindowElement.WindowSize:

                break;

            case WindowElement.Device:

                break;
        }
    }

}
