using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otamesi : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectObj = new List<GameObject>();   //�J�[�\���������Ă��������I�u�W�F�N�g�I�����āA�o�^

    private List<Vector3> selectPos = new List<Vector3>();  //selectObj�̍��W��o�^

    public RectTransform[] uiElements;  //RectTransform���ɂ���Width���g������

    private List<float> widths = new List<float>(); //selectObj�̕���o�^
    private float myWidth;  //�J�[�\�����g�̕�

    private int selectNum;  //�����݃J�[�\�����ǂ����w���Ă��邩

    RectTransform rect;
    
    // Start is called before the first frame update
    void Start()
    {
        //selectObj���I������Ă��Ȃ��Ƃ��A�R���\�[���ɏo��
        if (selectObj.Count == 0)
        {
            Debug.Log("selectObj�̒��g���Ȃ���");
        }

        //selectPos��selectObj�̍��W��o�^
        foreach(GameObject obj in selectObj)
        {
            if (obj != null)
            {
                selectPos.Add(obj.transform.position);
            }
        }

        //�e�I�u�W�F�N�g�̕���o�^
        foreach(RectTransform uiElement in uiElements)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }

        //�����l��0��
        selectNum = 0;

        rect = GetComponent<RectTransform>();


        myWidth = rect.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();   //�J�[�\���𓮂�������
        SelectMenu();
    }

    void MoveCursor()
    {
        //�I������Ă���I�u�W�F�N�g�̉E��(�I�u�W�F�N�g�̉E���g+�J�[�\���̍����g��)
        rect.position = new Vector3
            (selectPos[selectNum].x + (widths[selectNum] / 2) + (myWidth / 2),
            selectPos[selectNum].y,
            selectPos[selectNum].z);

        if (Input.GetKeyDown(KeyCode.UpArrow) && selectNum > 0)
        {
            selectNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && selectNum < selectObj.Count - 1)
        {
            selectNum++;
        }
    }

    void SelectMenu()
    {
        switch(selectNum)
        {
            case 0:

                break;
        }
    }

}
