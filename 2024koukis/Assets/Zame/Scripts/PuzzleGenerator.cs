using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    // 使用するパズルの駒
    [Header("パズルオブジェクト"), SerializeField] GameObject[] pObj;
    // パズルの駒を保存していく
    private Queue<GameObject> puzzleQueue=new Queue<GameObject>();

    // 乱数生成
    private System.Random random = new System.Random();

    // プレイヤースクリプト
    [SerializeField] P1Controller p1;
    [SerializeField] P2Controller p2;

    // キューの値を保存する
    private GameObject saveObj;
    /// <summary>
    /// パズルの駒をキューにいれる
    /// </summary>
    private void QueueSet()
    {
        // 取り敢えずキューには100個追加
        if (puzzleQueue.Count <= 100)
        {
            puzzleQueue.Enqueue(pObj[GetRandomNumber(0, pObj.Length)]);
        }
    }

    // 乱数取得
    private int GetRandomNumber(int min, int max)
    {
        return random.Next(min, max);  // 上限も含める場合
    }

    // パズルの駒を渡す
    private GameObject SendQueue()
    {
        return puzzleQueue.Dequeue();
    }

    // プレイヤースクリプトにオブジェクトを送り込む
    private void SendObj()
    {
        // キューの先頭から取り出す
        saveObj = SendQueue();
        p1.SetObj = saveObj; // p1
        p2.SetObj = saveObj; // p2
    }


    /// <summary>
    /// パズル生成してプレイヤーたちに送る
    /// </summary>
    public void GENERATOR()
    {
        QueueSet();
        SendObj();
    }
}
