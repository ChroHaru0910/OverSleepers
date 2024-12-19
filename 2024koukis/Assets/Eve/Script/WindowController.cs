using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] MoveCursor moveCursor;
    [SerializeField] WindowTitle windowTitle;
    [SerializeField] WindowSelect windowSelect;

    public List<GameObject> windowObj = new List<GameObject>();
    [SerializeField] public GameObject cursor;

    [HideInInspector]
    public int selectNum;
    [HideInInspector]
    public int selectMin = 0;
    [HideInInspector]
    public int selectMax;
    public enum WindowModeElement
    {
        None,
        Select,
        Sound,
        WindowSize,
    }
    [HideInInspector]
    public WindowModeElement windowState;
    private bool stateLock = false;

    private void Start()
    {
        windowState = WindowModeElement.None;
        selectNum = 0;
        selectMax = moveCursor.settingButtons.Length - 1;
    }

    public void Update()
    {
        if (stateLock) return;

        switch (windowState)
        {
            case WindowModeElement.None:
                Debug.Log("None");
                windowTitle.OpenMenu();
                break;

            case WindowModeElement.Select:
                Debug.Log("Select");
                windowSelect.SelectOption();
                moveCursor.Move();
                break;

            case WindowModeElement.Sound:

                break;

            case WindowModeElement.WindowSize:

                break;
        }
    }

    public void LockStateForFrames(int frames)
    {
        StartCoroutine(LockStateCoroutine(frames));
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
