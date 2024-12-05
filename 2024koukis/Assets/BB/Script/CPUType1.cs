using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUType1 : MonoBehaviour
{
    //とにかく一連鎖を組むタイプのCPUType1

    //変数まとめ
    #region Field

    //Type1のレベル指定用
    [SerializeField] int CpuLev = 1;

    //p2Controller
    P2Controller cpuScript;

    //p2Controllerのオブジェクトs
    GameObject CpuObj;

    #endregion

    public void CpuType1()
    {
        Init();
    }
    void Init()
    {
        //p2Controllerを取得
        CpuObj = GameObject.Find("GameManager");
        cpuScript = CpuObj.GetComponent<P2Controller>();
    }

}
