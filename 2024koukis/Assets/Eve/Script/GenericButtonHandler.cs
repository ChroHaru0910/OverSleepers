using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    // Start is called before the first frame update
    public void ToggleActive()
    {
        if (targetObj != null)
        {
            targetObj.SetActive(!targetObj.activeSelf);
        }
    }
}
