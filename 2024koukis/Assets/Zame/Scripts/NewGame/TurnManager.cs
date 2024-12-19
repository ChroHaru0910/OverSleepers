using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Field
    // プレイヤー
    public enum Turn
    {
        Player1,
        Player2,
        Members,
    }

    // 基本的にPlayer1が先攻
    // ただし乱数で後から決めなおす
    Turn turn=Turn.Player1;

    // PL1,2の順番でオブジェクトを設定
    [Header("アローオブジェクトを入れる"), SerializeField] GameObject[] arrowObj;

    // 乱数生成
    private System.Random random = new System.Random();

    // 乱数保存
    int randNum = 0;
    #endregion

    /// <summary>
    /// 誰のターンか返します
    /// </summary>
    public Turn GetTurn
    {
        // 値を返すと同時に矢印オブジェクトも処理する
        get {
            // 一度全て非表示
            FalseArrow();
            // 行動ターンの人の矢印オブジェクトを表示する
            arrowObj[(int)turn].SetActive(true);
            return turn;
        }
    }

    /// <summary>
    /// ターン変更
    /// </summary>
    public void TurnChange()
    {
        turn = turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;
    }

    /// <summary>
    /// 全て非表示にする
    /// </summary>
    private void FalseArrow()
    {
        for(int i=0;i<(int)Turn.Members;i++)
        {
            arrowObj[i].SetActive(false);
        }
    }

    /// <summary>
    /// 先攻を決める処理
    /// </summary>
    public void TurnSelect()
    {
        // 乱数を求めてその値で先手を決める
        randNum=random.Next(0, (int)Turn.Members);  // 上限も含める場合
        switch (randNum)
        {
            case 0:
                turn = Turn.Player1;
                break;
            case 1:
                turn = Turn.Player2;
                break;
        }
    }
}
