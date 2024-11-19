using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    // 生成した駒をまとめるオブジェクト
    [SerializeField] GameObject P1;
    // 生成するコマの情報
    private Queue<GameObject> puzzleQueue = new Queue<GameObject>();
    // 座標リスト
    List<LEFTWolrdVec2> listLEFT = new List<LEFTWolrdVec2>();
    // 自分の盤面のコマオブジェクト保存リスト
    List<GameObject> mypuzzleObj = new List<GameObject>();

    // 生成時の座標を計算
    private Vector2 Pv2;
    // 操作中のコマ
    GameObject ParentObj;

    // 自由落下する速度
    const float CONSTdropTime = 0.5f; // 最大時間
    float dropTime = 0.5f;            // 時間設定
    float dropTimerCnt = 0.0f;        // カウント用

    // 移動制限
    int Cy = 9;
    int Cx = 3;

    // 着地したか判断
    bool isFlg = true;
    // 負け判定
    bool gameLoseFlg = false;

    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;


    // GameManagerで呼び出し
    public void Game()
    {
        P1Input();
        Droppuzzle();
        ChainPuzzle();
    }

    /// <summary>
    /// 負け判定
    /// </summary>
    private void LoseMethod()
    {
        for (int i = 0; i < mypuzzleObj.Count; i++)
        {
            // 真ん中の一番上に置かれたら負け
            if (mypuzzleObj[i].transform.position.y >= listLEFT[9].posColumns[3].y &&
                mypuzzleObj[i].transform.position.x == listLEFT[9].posColumns[3].x)
            {
                gameLoseFlg = true;
            }
        }
    }

    public bool LOSEFLAG
    {
        get { return gameLoseFlg; }
    }

    /// <summary>
    /// 入力処理
    /// </summary>
    private void P1Input()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMoveLeft(ParentObj))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && CanMoveRight(ParentObj))
        {
            if (Cx == 9) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dropTime = 0.1f;  // ドロップ速度を速くする
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            dropTime = CONSTdropTime;  // 通常の落下速度に戻す
        }
    }

    /// <summary>
    /// 盤面にコマを生成する
    /// </summary>
    public void GeneObj()
    {
        // 連鎖中の場合は生成しない
        if (puzzleCleared) { return; }
        timer = 0;
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P1.transform;
        mypuzzleObj.Add(ParentObj);
        isFlg = false;
    }

    // 落下処理
    private void Droppuzzle()
    {
        if (isFlg) { return; }
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                isFlg = true;
            }
            else
            {
                if (SetPuzzle(ParentObj))
                {
                    ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y);
                    Cy--;
                }
                else
                {
                    CheckAndClearPuzzle();
                    LoseMethod();
                    isFlg = true;
                }
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
                return false;
            }
        }
        return true;
    }

    // 左右に駒が存在しないかをチェックする
    private bool CanMoveLeft(GameObject obj)
    {
        if (Cx <= 0) return false;

        float targetX = listLEFT[Cy].posColumns[Cx - 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMoveRight(GameObject obj)
    {
        if (Cx >= listLEFT[Cy].posColumns.Length - 1) return false;

        float targetX = listLEFT[Cy].posColumns[Cx + 1].x;
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.y, obj.transform.position.y) &&
                Mathf.Approximately(puzzle.transform.position.x, targetX))
            {
                return false;
            }
        }
        return true;
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
    public void RecieveList(List<LEFTWolrdVec2> list)
    {
        listLEFT = list;
    }

    /// <summary>
    /// 駒が消えるべきかチェックし、消す処理
    /// </summary>
    private void CheckAndClearPuzzle()
    {
        // 横方向のチェック
        for (int y = 0; y < listLEFT.Count; y++)
        {
            for (int x = 0; x < listLEFT[y].posColumns.Length; x++)
            {
                if (ClearPuzzleIfSurrounded(x, y, 1, 0)) // 横方向のチェック (dx=1, dy=0)
                {
                    puzzleCleared = true;
                }
            }
        }

        // 縦方向のチェック
        for (int x = 0; x < listLEFT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listLEFT.Count; y++)
            {
                if (ClearPuzzleIfSurrounded(x, y, 0, 1)) // 縦方向のチェック (dx=0, dy=1)
                {
                    puzzleCleared = true;
                }
            }
        }
    }


    // 連鎖処理
    private void ChainPuzzle()
    {
        // 駒が消えた場合、コマを下に詰めさらに消せる場所を探す
        if (puzzleCleared)
        {
            switch (num)
            {
                case 0:
                    ShiftPuzzlesDown();
                    timer += Time.deltaTime;
                    if (timer >= 0.25f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 1:
                    CheckAndClearPuzzle();
                    timer += Time.deltaTime;
                    if (timer >= 0.25f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 2:
                    ShiftPuzzlesDown();
                    timer = 0.25f; // 次の処理スキップ
                    puzzleCleared = false;
                    num = 0;
                    break;
            }
        }
    }

    /// <summary>
    /// 指定された位置の2つの駒を消すかどうかを判定し、消す処理
    /// </summary>
    /// <param name="x">チェックするX座標</param>
    /// <param name="y">チェックするY座標</param>
    /// <param name="dx">X軸方向の移動量</param>
    /// <param name="dy">Y軸方向の移動量</param>
    /// <returns>駒が消えた場合true</returns>
    private bool ClearPuzzleIfSurrounded(int x, int y, int dx, int dy)
    {
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        int oppositeX = x + dx * 2;
        int oppositeY = y + dy * 2;

        GameObject middlePuzzle = GetPuzzleAt(x + dx, y + dy);

        GameObject endPuzzle = GetPuzzleAt(oppositeX, oppositeY);

        if (endPuzzle == null) return false;

        Color startColor = GetPuzzleColor(startPuzzle);
        Color endColor = GetPuzzleColor(endPuzzle);

        if (startColor == endColor && startColor != Color.clear)
        {

            if (middlePuzzle != null && GetPuzzleColor(middlePuzzle) != startColor)
            {
                RemovePuzzle(startPuzzle);
                RemovePuzzle(middlePuzzle);
                RemovePuzzle(endPuzzle);
                return true; // 駒を消した場合はtrueを返す
            }
        }

        return false;
    }
    private void RemovePuzzle(GameObject puzzle)
    {
        if (mypuzzleObj.Contains(puzzle))
        {
            mypuzzleObj.Remove(puzzle);
            Destroy(puzzle);
        }
    }
    /// <summary>
    /// 駒の色を取得する
    /// </summary>
    private Color GetPuzzleColor(GameObject puzzle)
    {
        SpriteRenderer spriteRenderer = puzzle.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.color : Color.clear;
    }

    /// <summary>
    /// コマを下にシフトさせる
    /// </summary>
    private void ShiftPuzzlesDown()
    {
        for (int x = 0; x < listLEFT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listLEFT.Count; y++)
            {
                Vector2 currentPos = listLEFT[y].posColumns[x];
                GameObject currentPuzzle = GetPuzzleAtPosition(currentPos);

                if (currentPuzzle == null)
                {
                    for (int aboveY = y + 1; aboveY < listLEFT.Count; aboveY++)
                    {
                        Vector2 abovePos = listLEFT[aboveY].posColumns[x];
                        GameObject abovePuzzle = GetPuzzleAtPosition(abovePos);

                        if (abovePuzzle != null)
                        {
                            Vector2 targetPosition = currentPos;
                            abovePuzzle.transform.position = targetPosition;
                            UpdatePuzzlePosition(abovePuzzle, abovePos, targetPosition);
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 指定位置のパズルオブジェクトを取得
    /// </summary>
    private GameObject GetPuzzleAt(int x, int y)
    {
        if (y < 0 || y >= listLEFT.Count || x < 0 || x >= listLEFT[y].posColumns.Length)
            return null;

        Vector2 targetPos = listLEFT[y].posColumns[x];
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, targetPos.x) &&
                Mathf.Approximately(puzzle.transform.position.y, targetPos.y))
            {
                return puzzle;
            }
        }
        return null;
    }

    /// <summary>
    /// 特定位置のパズルを取得
    /// </summary>
    private GameObject GetPuzzleAtPosition(Vector2 position)
    {
        foreach (GameObject puzzle in mypuzzleObj)
        {
            if (Mathf.Approximately(puzzle.transform.position.x, position.x) &&
                Mathf.Approximately(puzzle.transform.position.y, position.y))
            {
                return puzzle;
            }
        }
        return null;
    }

    /// <summary>
    /// パズルの位置を更新する
    /// </summary>
    private void UpdatePuzzlePosition(GameObject puzzle, Vector2 oldPos, Vector2 newPos)
    {
        puzzle.transform.position = newPos;
    }
}