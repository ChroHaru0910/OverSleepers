using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    public WindowController windowController;

    [SerializeField] GameObject cursor;

    [HideInInspector]
    public List<Vector3> buttonPos = new List<Vector3>();
    [HideInInspector]
    public List<float> widths = new List<float>();
    private float cursorWidth;


    private RectTransform cursorRect;
    public RectTransform[] settingButtons;
    public RectTransform[] soundButtons;

    //[SerializeField] GameObject cursor;
    void Start()
    {
        cursorRect = cursor.GetComponent<RectTransform>();
        cursorWidth = cursorRect.rect.width;

        for (int i = 0; i < settingButtons.Length; i++)
        {
            buttonPos.Add(settingButtons[i].transform.localPosition);
        }
        foreach (RectTransform uiElements in settingButtons)
        {
            float width = uiElements.rect.width;
            widths.Add(width);
        }

    }

    public void Move()
    {
        cursor.transform.localPosition = new Vector3
            (buttonPos[windowController.selectNum].x + (widths[windowController.selectNum] / 2) + (cursorWidth / 2),
            buttonPos[windowController.selectNum].y,
            buttonPos[windowController.selectNum].z);
    }
}
