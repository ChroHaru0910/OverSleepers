using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSelectScene : MonoBehaviour
{
    private bool openSetting;
    private bool receiveFlag;

    [SerializeField] GameObject settingObj;

    // Start is called before the first frame update
    void Start()
    {
        openSetting = false;
        receiveFlag = GetComponent<BackButton>().openFlag;
        settingObj.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        openSetting = receiveFlag;
        OpenMenu();
    }

    void OpenMenu()
    {
        if (openSetting == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settingObj.SetActive(true);
                openSetting = true;
            }
            else if (Input.anyKeyDown)
            {
                //セレクトシーン移行
                //SceneManager.LoadScene("");
            }
        }
        else if (openSetting == true && Input.GetKeyDown(KeyCode.Escape))
        {
            settingObj.SetActive(false);
            openSetting = false;
        }
    }

}
