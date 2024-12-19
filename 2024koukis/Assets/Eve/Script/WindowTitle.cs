using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowTitle : MonoBehaviour
{
    public WindowController windowController;

    [SerializeField] GameObject windowSetting;
    //[SerializeField] GameObject cursor;


    private void Start()
    {

    }

    public void OpenMenu()
    {
        if(!windowSetting.activeSelf)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                windowSetting.SetActive(true);
                windowController.cursor.SetActive(true);
                windowController.windowState = WindowController.WindowModeElement.Select;
                windowController.LockStateForFrames(10);
            }
            else if (Input.anyKey)
            {
                //SceneManager.LoadScene("");
            }
        }
    }
}
