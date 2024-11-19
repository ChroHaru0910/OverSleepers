using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    // 生成した駒をまとめるオブジェクト
    [SerializeField] GameObject P2;
    // 生成するコマの情報
    private Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    // 座標リスト
    List<RIGHTWolrdVec2> listRIGHT = new List<RIGHTWolrdVec2>();
    // 自分の盤面のコマオブジェクト保存リスト
    List<GameObject> mypuzzleObj = new List<GameObject>();

    // 生成時の座標を計算
    private Vector2 Pv2;
    // 操作中のコマ
    GameObject ParentObj;

    // 自由落下する速度
    const float dropTime = 0.5f;
    float dropTimerCnt = 0.0f;

    // 移動制限
    int Cy = 9;
    int Cx = 3;

    // 着地したか判断
    bool isFlg = true;

    // GameManagerで呼び出し
    public void Game()
    {
        P2Input();
        Droppuzzle();
    }

    public void P2Input()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanMoveLeft(ParentObj))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.D) && CanMoveRight(ParentObj))
        {
            if (Cx == 9) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 下に移動の処理（例: ドロップ速度を早めるなど）
        }
    }

    /// <summary>
    /// 盤面にコマを生成する
    /// </summary>
    public void GeneObj()
    {
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P2.transform;
        mypuzzleObj.Add(ParentObj);
        isFlg = false;
    }


    // 自由落下処理
    private void Droppuzzle()
    {
        if (isFlg) { return; }
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
                    ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y);
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
    // 現在操作中のコマの下に別のコマが存在するか
    private bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 0.7f;

        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, targetY) &&
                Mathf.Approximately(puzzle.transform.position.x, obj.transform.position.x))
            {
                Debug.Log("駒の一個下に別の駒が存在します。");
                return false;
            }
        }
        return true;
    }

    // 左右に駒が存在しないかをチェックする
    private bool CanMoveLeft(GameObject obj)
    {
        if (Cx <= 0) return false;

        float targetX = listRIGHT[Cy].posColumns[Cx - 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false; // 左に駒があるため移動不可
            }
        }
        return true; // 左に駒がないため移動可
    }
    private bool CanMoveRight(GameObject obj)
    {
        if (Cx >= listRIGHT[Cy].posColumns.Length - 1) return false;

        float targetX = listRIGHT[Cy].posColumns[Cx + 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false; // 右に駒があるため移動不可
            }
        }
        return true; // 右に駒がないため移動可
    }

    /// <summary>
    /// 生成するパズルの駒を受け取る
    /// </summary>
    public GameObject SetObj
    {
        set { puzzleQueue.Enqueue(value); }
    }
    /// <summary>
    /// 次のコマを生成するタイミングを返す
    /// </summary>
    public bool IsFLAG { get { return isFlg; } }

    /// <summary>
    /// 盤面の座標を貰う
    /// </summary>
    /// <param name="list">座標の入ったリスト</param>
    public void RecieveList(List<RIGHTWolrdVec2> list)
    {
        listRIGHT = list;
    }
}
