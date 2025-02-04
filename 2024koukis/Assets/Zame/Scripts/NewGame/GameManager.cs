using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region field
    // テキスト表示のスクリプト
    [SerializeField] TextManager textMng;

    // ターンを管理
    [SerializeField] TurnManager turnMng;
    TurnManager.Turn turn;

    // ボードデータ管理
    [SerializeField] BoardManager boardMng;
    List<WolrdVec2> vec2 = new List<WolrdVec2>();

    // パズルのコマを管理
    [SerializeField] KomaManager komaMng;

    // 結果を反映させる
    [SerializeField] WinStar winStar;

    // フェードを実装のために使うイメージ
    [SerializeField] Image image;

    const float spd = 0.01f;        // フェード速度
    const float timeSpan = 0.5f;    // 間隔
    float spdFade = 0;              // フェード値
    float timer = 0;                // 加算用

    // フェードさせるクラス
    ColFade fade;

    // ゲーム状態管理の列挙体
    enum GAME
    {
        READY,
        START,
        PLAY,
        RESULT,
    }

    // 列挙体の変数
    private GAME gameState;
    #endregion

    void Update()
    {
        // ゲームループはここで
        switch(gameState)
        {
            case GAME.READY:  // ゲームの状態はready 正しくデータをもらえたらスタートしていく
                StateReady();
                break;
            case GAME.START:  // ゲームスタートの合図を送る
                StateStart();
                break;
            case GAME.PLAY:   // ゲーム開始
                StatePlay();
                break;
            case GAME.RESULT: // 勝敗
                StateResult();
                break;
        }
    }

    // データのセット
    private bool SetUpStart()
    {
        fade = new ColFade();        // インスタンス生成

        turnMng.TurnSelect();        // 先攻を決める

        winStar.SetWin();            // 勝利数を反映

        // ターンを貰う
        turn = turnMng.GetTurn;

        // ボード座標取得
        boardMng.BoardSet();
        vec2 = boardMng.ReturnList();
        
        // テキストに反映させる
        textMng.SystemSetText(turn.ToString()+"が先攻です");

        // 座標を渡す
        komaMng.RecieveList(vec2);
        return true;
    }

    // gameStateの処理内容
    #region State
    /// <summary>
    /// スタート前の準備
    /// </summary>
    private void StateReady()
    {
        if (SetUpStart())
        {
            // フェードアウトさせる
            spdFade += spd;
            fade.ImgFade(image, -spdFade);
            if (image.color.a <= 0)
            {
                // 次に進む
                gameState = GAME.START;
            }
        }
    }

    /// <summary>
    /// ゲームスタートの合図を送る
    /// </summary>
    private void StateStart()
    {
        timer += Time.deltaTime;
        if(timer>=timeSpan)
        {
            textMng.SystemSetText(true);  // 文字を消す
            timer = 0;                    // タイマーリセット
            gameState = GAME.PLAY;        // 次に進む
        }
    }


    /// <summary>
    ///  ゲーム開始
    /// </summary>
    private void StatePlay()
    {
        if (komaMng.CanNextPuzzle&&!komaMng.IsChained)
        {
            // ターンを貰う
            turn = turnMng.GetTurn;
            // ターン交代
            turnMng.TurnChange();

            komaMng.GeneObj(turn);

        }
        komaMng.Game();
    }

    /// <summary>
    /// 勝敗表示
    /// </summary>
    private void StateResult()
    {
    
    }
    #endregion
}
