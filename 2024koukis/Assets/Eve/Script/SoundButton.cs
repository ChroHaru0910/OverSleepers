using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public GameObject soundObj;
    public void OnClickButton()
    {
        soundObj.SetActive(true);
    }
}
