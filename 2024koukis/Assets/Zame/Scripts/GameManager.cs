using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region field
    // ここの拡張性
    // パズルの盤面を保存する
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // フェードさせるUI
    [SerializeField] Image img;
    // フェード速度
    [SerializeField] float spdImg=0.03f;
    // フェードクラス
    ColFade colClass;

    // ゲーム開始の合図を記載
    [SerializeField] Text Readytext;

    // 勝敗反映テキスト
    [SerializeField] Text Pl1text;
    [SerializeField] Text Pl2text;
    // 勝敗プレイヤー
    // TRUE=PL1,FALSE=PL2
    // 負けたプレイヤで判断
    bool isLose = false;

    // 座標を保存するリスト
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();

    // 時間
    float timer = 0;
    // ある処理から次の処理にまで向かう間隔
    [SerializeField] const float maxTime = 1.0f;

    // 生成処理
    [SerializeField] PuzzleGenerator generator;

    // プレイヤー
    [SerializeField] P1Controller p1Controller;
    [SerializeField] P2Controller p2Controller;

    // jsonデータ
    [SerializeField] VariableManager variable;
    // 変数の値をデータをもとに変更
    [SerializeField] VariableValue value;

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

    void Start()
    {
        // データ読み込み
        variable.LoadJson();

        // 変数のセット
        value.SETUP();

        // プレイヤー１に値をセット
        p1Controller.SetUp=value.DROPSPD;

        // インスタンス生成
        colClass = new ColFade();

        // 透明度半分
        colClass.ImgFade(img, 0.5f);

        // 合図の準備
        Readytext.text = "レディー!!";

        // スイッチ処理準備
        gameState = GAME.READY;

        // 盤面保存
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();

        // 保存したリストを貰い受ける
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();

        // 駒を二回生成（ネクスト表示のため）
        generator.GENERATOR();
        generator.GENERATOR();

        // プレイヤーに座標リストをプレゼント
        p1Controller.RecieveList(listLEFT);
        p2Controller.RecieveList(listRIGHT);

        // ネクスト表示の準備
        p1Controller.STARTSET();
        p2Controller.STARTSET();
    }

    void Update()
    {
        // ゲームループはここで
        switch(gameState)
        {
            case GAME.READY: // ゲームの状態はready 正しくデータをもらえたらスタートしていく
                 // startメソッドでデータが正しく貰えてない場合に
                if (listLEFT.Count == 0 || listRIGHT.Count == 0)
                {
                    Debug.Log("再度リストを貰います");
                    // 盤面保存
                    boardLEFT.LeftSet();
                    boardRIGHT.RightSet();
                    // 保存したリストを貰い受ける
                    listLEFT = boardLEFT.ReturnList();
                    listRIGHT = boardRIGHT.ReturnList();
                }

                timer += Time.deltaTime;
                if (timer >= maxTime)
                {
                    // タイマーの値を元通りに
                    timer = 0;
                    gameState = GAME.START;
                }
                break;
            case GAME.START: // ゲームスタートの合図を送る
                {
                    if (img.color.a >= 0)
                    {
                        colClass.ImgFade(img, -spdImg);
                    }
                    else
                    {
                        Readytext.text = "スタート!!";
                        timer += Time.deltaTime;
                        if (timer >= maxTime)
                        {
                            // タイマーの値を元通りに
                            timer = 0;

                            // 入力の受付開始
                            gameState = GAME.PLAY;
                            Destroy(Readytext);
                        }
                    }
                }
                break;
            case GAME.PLAY: // ゲーム開始
                // 勝敗
                if (p1Controller.IsLose)
                {
                    isLose = true;
                    gameState = GAME.RESULT;
                    
                    return;
                }
                // p2の死亡処理はデバッグ中のためコメント
                //else if (p2Controller.LOSEFLAG)
                //{
                //    winLose = false;
                //    game = GAME.RESULT;
                //}

                // 駒を生成
                generator.GENERATOR();

                // それぞれの盤面にコマを生成
                if (p1Controller.CanNextPuzzle)
                {
                    // ネクスト表示を更新して新規生成
                    p1Controller.GeneObj();
                }
                if (p2Controller.CanNextPuzzle)
                {
                    // ネクスト表示を更新して新規生成
                    p2Controller.GeneObj();
                }

                // プレイヤー更新
                p1Controller.Game();
                p2Controller.Game();
                break;
            case GAME.RESULT:
                // フェード
                if (img.color.a <= 0.5f)
                {
                    colClass.ImgFade(img, spdImg);
                }
                else
                {
                    // 勝敗のテキスト
                    if(isLose)
                    {
                        Pl1text.text = "LOSE";
                        Pl2text.text = "WIN!!";
                    }
                    else
                    {
                        Pl1text.text = "WIN!!";
                        Pl2text.text = "LOSE";
                    }
                }
                Debug.Log("結果発表");

                // ここから再戦できるようにする
                break;
        }
    }
}
