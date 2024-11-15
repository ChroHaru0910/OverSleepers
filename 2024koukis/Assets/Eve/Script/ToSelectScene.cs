using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToSelectScene : MonoBehaviour
{
    [SerializeField] GameObject settingObj;

    // Start is called before the first frame update
    void Start()
    {
        settingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (settingObj.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            settingObj.SetActive(true);
        }
        else if (settingObj.activeSelf == true)
        {
            OpenMenu();
        }
    }

    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingObj.SetActive(false);
        }
    }

}
