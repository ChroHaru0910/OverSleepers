using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otamesi : MonoBehaviour
{
    [SerializeField] private GenericButtonHandler geneButton;
    [SerializeField] private BGMTxet bgmText;
    [SerializeField] private SEText seText;

    [SerializeField] private List<GameObject> selectObj = new List<GameObject>();   //カーソルを持っていきたいオブジェクト選択して、登録
    [SerializeField] private List<GameObject> windowObj = new List<GameObject>();   //ウィンドウオブジェリスト

    private List<Vector3> selectPos = new List<Vector3>();  //selectObjの座標を登録

    public RectTransform[] uiElements;  //RectTransform内にあるWidthを使うため

    private List<float> widths = new List<float>(); //selectObjの幅を登録
    private float myWidth;  //カーソル自身の幅

    private int selectNum;  //今現在カーソルがどこを指しているか

    RectTransform rect;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("re");
        }

        if (bgmText == null)
        {
            bgmText = FindObjectOfType<BGMTxet>();
        }
        if (seText == null)
        {
            seText = FindObjectOfType<SEText>();
        }
        if (geneButton == null)
        {
            geneButton = FindObjectOfType<GenericButtonHandler>();
        }

        //selectObjが選択されていないとき、コンソールに出力
        if (selectObj.Count == 0)
        {
            Debug.Log("selectObjの中身がないよ");
        }

        //selectPosにselectObjの座標を登録
        foreach(GameObject obj in selectObj)
        {
            if (obj != null)
            {
                selectPos.Add(obj.transform.position);
            }
        }

        //各オブジェクトの幅を登録
        foreach(RectTransform uiElement in uiElements)
        {
            float width = uiElement.rect.width;

            widths.Add(width);
        }

        //初期値は0に
        selectNum = 0;

        rect = GetComponent<RectTransform>();


        myWidth = rect.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();   //カーソルを動かす処理
        SelectMenu();
    }

    void MoveCursor()
    {
        //選択されているオブジェクトの右横(オブジェクトの右半身+カーソルの左半身分)
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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectNum)
            {
                case 0:
                    geneButton.ToggleActive();
                    break;
                case 1:
                    bgmText.DecButton();
                    break;
                case 2:
                    bgmText.IncButton();
                    break;
                case 3:
                    seText.seNum += seText.changeNum;
                    break;
                case 4:
                    seText.DecButton();
                    break;
                   
            }
        }
        
    }

}
