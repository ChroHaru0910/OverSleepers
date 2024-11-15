using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public bool openFlag;

    Button button;
    [SerializeField] GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        openFlag = true;

        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchObj);
    }

    // Update is called once per frame
    void SwitchObj()
    {
        obj.SetActive(false);
        openFlag = false;
    }
    
        
    

    
}
