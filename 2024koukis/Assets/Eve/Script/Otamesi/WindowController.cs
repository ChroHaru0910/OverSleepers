using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public List<GameObject> windowObj = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < windowObj.Count; i++)
        {
            windowObj[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            windowObj[0].SetActive(true);
        }
    }

}
