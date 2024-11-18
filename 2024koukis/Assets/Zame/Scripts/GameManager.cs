using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region field
    // パズルの盤面を保存する
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // フェードさせるUI
    [SerializeField] Image img;
    // フェード速度
    [SerializeField] float spdImg=0.03f;
    // フェードクラス
    ColFade col;

    // ゲーム開始の合図を記載
    [SerializeField] Text text;

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

    // ゲーム状態管理の列挙体
    enum GAME
    {
        READY,
        START,
        PLAY,
        RESULT,
    }

    // 列挙体の変数
    private GAME game;
    #endregion

    void Start()
    {
        // インスタンス生成
        col = new ColFade();

        // 透明度半分
        col.ImgFade(img, 0.5f);

        // 合図の準備
        text.text = "レディー!!";

        // スイッチ処理準備
        game = GAME.READY;

        // 盤面保存
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();

        // 保存したリストを貰い受ける
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();

        // 駒を生成
        generator.GENERATOR();

        // プレイヤーに座標リストをプレゼント
        p1Controller.RecieveList(listLEFT);
        p2Controller.RecieveList(listRIGHT);

    }

    // Update is called once per frame
    void Update()
    {
        // ゲームループはここで
        switch(game)
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
                    game = GAME.START;
                }

                break;
            case GAME.START: // ゲームスタートの合図を送る
                {
                    if (img.color.a >= 0)
                    {
                        col.ImgFade(img, -spdImg);
                    }
                    else
                    {
                        text.text = "スタート！！";
                        timer += Time.deltaTime;
                        if (timer >= maxTime)
                        {
                            // タイマーの値を元通りに
                            timer = 0;

                            // 入力の受付開始
                            game = GAME.PLAY;
                            Destroy(text);
                        }
                    }
                }
                break;
            case GAME.PLAY: // ゲーム開始
                // 駒を生成
                if(p1Controller.LOSEFLAG)
                {
                    game = GAME.RESULT;
                    return;
                }
                generator.GENERATOR();
                if (p1Controller.IsFLAG)
                {
                    p1Controller.GeneObj();
                }
                if (p2Controller.IsFLAG)
                {
                    p2Controller.GeneObj();
                }
                p1Controller.Game();
                p2Controller.Game();
                break;
            case GAME.RESULT:
                Debug.Log("結果発表");
                break;
        }
    }
}
