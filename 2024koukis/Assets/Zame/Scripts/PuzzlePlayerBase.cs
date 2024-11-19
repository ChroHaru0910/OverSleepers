using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlayerBase : MonoBehaviour
{
    // プレイヤーオブジェクト
    [SerializeField] protected GameObject player;

    // 駒のキューとリスト
    protected Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    protected List<GameObject> mypuzzleObj = new List<GameObject>();

    // 駒の親オブジェクト
    protected GameObject ParentObj;

    // 落下時間とタイマー
    protected const float dropTime = 0.5f;
    protected float dropTimerCnt = 0.0f;

    // 盤面のY座標とX座標
    protected int Cy = 9;
    protected int Cx = 3;

    // フラグと位置リスト
    protected bool isFlg = true;
    protected List<Vector2> posColumns = new List<Vector2>();

    // 新しい駒をキューに追加するプロパティ
    public GameObject SetObj
    {
        set { puzzleQueue.Enqueue(value); }
    }

    // フラグがtrueかどうかを返すプロパティ
    public bool IsFLAG { get { return isFlg; } }

    // ゲームのループを行う
    public virtual void Game()
    {
        HandleInput();
        Droppuzzle();
    }

    // 各コントローラで入力処理を実装
    protected virtual void HandleInput() { }

    // 自由落下処理
    protected void Droppuzzle()
    {
        if (isFlg) return;
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                isFlg = true;
                Debug.Log("設置");
            }
            else
            {
                if (SetPuzzle(ParentObj))
                {
                    ParentObj.transform.position = new Vector2(posColumns[Cx].x, posColumns[Cx].y);
                }
                else
                {
                    isFlg = true;
                    Debug.Log("駒が下にブロックされて設置されました");
                    return;
                }
                Cy--;
            }
        }
    }

    // 駒の下に別の駒が存在するかをチェック
    protected bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, targetY) &&
                Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x))
            {
                return false;
            }
        }
        return true;
    }

    // 駒が左に移動できるかどうか
    protected bool CanMoveLeft(GameObject obj)
    {
        float targetX = obj.transform.position.x - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetX) &&
                Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y))
            {
                return false;
            }
        }
        return true;
    }

    // 駒が右に移動できるかどうか
    protected bool CanMoveRight(GameObject obj)
    {
        float targetX = obj.transform.position.x + 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetX) &&
                Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y))
            {
                return false;
            }
        }
        return true;
    }

    // 駒が下に移動できるかどうか
    protected bool CanMoveDown(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x) &&
                Mathf.Approximately(puzzle.transform.position.y, targetY))
            {
                return false;
            }
        }
        return true;
    }

    // 駒を生成するメソッド（Geneメソッド）
    protected void Gene(GameObject puzzlePrefab)
    {
        if (puzzleQueue.Count > 0)
        {
            ParentObj = Instantiate(puzzleQueue.Dequeue());
            ParentObj.transform.position = new Vector2(posColumns[Cx].x, posColumns[Cx].y);
            mypuzzleObj.Add(ParentObj);
            isFlg = false; // 駒を生成したら、落下を開始する
        }
    }
}
