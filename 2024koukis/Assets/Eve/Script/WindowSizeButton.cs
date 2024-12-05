using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSizeButton : MonoBehaviour
{
    [SerializeField] GameObject windowSizeScreen;
    public void OnClickButton()
    {
        windowSizeScreen.SetActive(true);
    }
}
