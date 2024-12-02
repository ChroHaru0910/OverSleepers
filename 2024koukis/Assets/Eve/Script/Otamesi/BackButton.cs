using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject obj;   
    public void OnClickBackButton()
    {
        obj.SetActive(false);
    }
}
