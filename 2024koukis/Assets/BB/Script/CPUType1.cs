using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUType1 : MonoBehaviour
{
    //�Ƃɂ�����A����g�ރ^�C�v��CPUType1

    //�ϐ��܂Ƃ�
    #region Field

    //Type1�̃��x���w��p
    [SerializeField] int CpuLev = 1;

    //p2Controller
    P2Controller cpuScript;

    //p2Controller�̃I�u�W�F�N�gs
    GameObject CpuObj;

    #endregion

    public void CpuType1()
    {
        Init();
    }
    void Init()
    {
        //p2Controller���擾
        CpuObj = GameObject.Find("GameManager");
        cpuScript = CpuObj.GetComponent<P2Controller>();
    }

}
