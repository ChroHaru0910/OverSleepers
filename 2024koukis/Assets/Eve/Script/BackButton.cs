using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject windowSetting;
    [SerializeField] GameObject cursor;

    public void OnClickButton()
    {
        windowSetting.SetActive(false);
        cursor.SetActive(false);
    }
}
