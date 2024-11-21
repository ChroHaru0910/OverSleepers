using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private List<GameObject> windowObj = new List<GameObject>();

    private int windowNum;

    private int selectNum;      //カーソル移動の際使うもの


    // Start is called before the first frame update
    void Start()
    {
        windowObj[0].SetActive(true);
        windowObj[1].SetActive(false);
        windowObj[2].SetActive(false);

        windowNum = 0;

        //for (int i = 0; i < windowObj.Count; i++)
        //{
        //    Debug.Log("wawa");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

}
