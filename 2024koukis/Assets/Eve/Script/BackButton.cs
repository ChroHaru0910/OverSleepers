using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] GameObject cursor;
    public void OnClickButton()
    {
        obj.SetActive(false);
        cursor.SetActive(false);
    }
}
