using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    #region Field
    // 勝敗を表示する
    [Header("勝ったプレイヤーを表示する"), SerializeField] Text resultText;
    // ゲームの準備等プレイヤーに知らせるシステムテキスト
    [Header("システムテキスト"),SerializeField] Text systemText;
    #endregion

    #region Function
    /// <summary>
    /// テキストセット
    /// </summary>
    /// <param name="text">表示したい文字</param>
    public void SystemSetText(string text)
    {
        systemText.text = text;
    }
    /// <summary>
    /// 表示しているシステムテキストを消す処理
    /// </summary>
    /// <param name="reset">消すならtrueを渡して</param>
    public void SystemSetText(bool reset)
    {
        if (reset) { systemText.text = ""; }
    }

    /// <summary>
    /// 勝ったプレイヤーを表示する
    /// </summary>
    /// <param name="name">勝者プレイヤーの名前を渡す</param>
    public void WinText(string name)
    {
        resultText.text = name + "WIN";
    }

    public void Draw()
    {
        resultText.text = "DRAW";
    }
    #endregion
}
