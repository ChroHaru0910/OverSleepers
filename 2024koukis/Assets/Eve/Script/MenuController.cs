using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] public GameObject menuObj;
    [SerializeField] public GameObject cursorObj;

    // Start is called before the first frame update
    void Start()
    {
        menuObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuObj.activeSelf == false)
        {
            menuObj.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuObj.activeSelf == true)
        {
            menuObj.SetActive(false);
        }
    }
}
