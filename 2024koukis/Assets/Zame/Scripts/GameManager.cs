using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region field
    // パズルの盤面を保存する
    [SerializeField] LEFT_PuzzleBoard boardLEFT;
    [SerializeField] RIGHT_PuzzleBoard boardRIGHT;

    // 座標を保存するリスト
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();

    enum GAME
    {
        READY,
        START,
        RESULT,
    }

    private GAME game;
    #endregion

    void Start()
    {
        game = GAME.READY;
        // 盤面保存
        boardLEFT.LeftSet();
        boardRIGHT.RightSet();
        // 保存したリストを貰い受ける
        listLEFT = boardLEFT.ReturnList();
        listRIGHT = boardRIGHT.ReturnList();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームループはここで
        switch(game)
        {
            case GAME.READY: // ゲームの状態はready 正しくデータをもらえたらスタートしていく
            // startメソッドでデータが正しく貰えてない場合に
            if (listLEFT.Count ==0 || listRIGHT.Count==0)
                {
                    Debug.Log("再度リストを貰います");
                    // 盤面保存
                    boardLEFT.LeftSet();
                    boardRIGHT.RightSet();
                    // 保存したリストを貰い受ける
                    listLEFT = boardLEFT.ReturnList();
                    listRIGHT = boardRIGHT.ReturnList();
                }
            else
                {
                    Debug.Log(listLEFT[0].posColumns[0]);
                    game = GAME.START;
                }
                break;
            case GAME.START: // ここからゲーム開始入力等の受け付けはここから
                {

                }
                break;
        }
    }
}
