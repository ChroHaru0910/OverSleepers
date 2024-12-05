using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public abstract class PuzzlePlayerBase : MonoBehaviour
{
    #region Fields
    protected List<Vector2> boardPositions; // 座標リスト
    protected bool isLose;                  // 敗北フラグ
    protected bool canNextPuzzle;           // 次の駒を生成できるか
    protected float dropSpeed;              // 駒の落下速度
    protected GameObject currentPiece;      // 現在操作中の駒

    public bool IsLose => isLose;           // 敗北フラグのプロパティ
    public bool CanNextPuzzle => canNextPuzzle; // 次の駒生成フラグのプロパティ
    public float SetUp { set => dropSpeed = value; } // 外部から落下速度を設定
    public GameObject SetObj { set => currentPiece = value; } // 駒をセット
    #endregion

    #region Abstract Methods
    public abstract void Game();            // ゲーム処理の更新
    public abstract void RecieveList(List<Vector2> positions); // 座標リストの受け取り
    public abstract void GeneObj();         // 駒の生成
    public abstract void STARTSET();        // ゲーム開始時の準備
    #endregion
}
