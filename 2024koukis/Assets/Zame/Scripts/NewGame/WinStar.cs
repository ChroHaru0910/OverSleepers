using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStar : MonoBehaviour
{
    #region Field
    // テキストにて何ラウンド勝ったか表示する
    [Header("win数を表示するテキスト"), SerializeField] Text[] win;

    // お互いのプレイヤーの勝利数
    int[] num = new int[2];

    // テキストマネージャーに勝者を渡す
    [SerializeField] TextManager textMng;

    [SerializeField] SaveInt intData;
    #endregion

    #region Function
    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    /// <param name="turn">勝者の名前</param>
    public void Win(TurnManager.Turn turn)
    {
        // 保存したデータを読み込む
        num = intData.GetInt;
        // 勝利プレイヤーの名前をテキストに反映
        textMng.WinText(turn.ToString());
        // 勝利したので星マークを獲得
        num[(int)turn]++;
        // 追加してから保存する
        intData.GetInt = num;
        intData.SaveData();
        // プレイヤーの星マークの数値を反映
        win[(int)turn].text =num[(int)turn].ToString();
    }

    // GameManagerにて最初に読み込む
    /// <summary>
    /// 勝利星マークの反映処理
    /// </summary>
    public void SetWin()
    {
        intData.LoadData();
        num = intData.GetInt;
        for(int i=0;i<num.Length;i++)
        {
            win[i].text = num[i].ToString();
        }
    }

    /// <summary>
    /// ファーストラウンドに実行か
    /// もしくはラウンド終了後再戦する前に実行
    /// </summary>
    public void WinsReset()
    {
        Debug.Log("データをリセットします。");

        for(int i=0;i<(int)TurnManager.Turn.Members;i++)
        {
            num[i] = 0;
        }
        // 保存する
        intData.GetInt = num;
        intData.SaveData();
    }
    #endregion
}
