using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2Controller : MonoBehaviour
{
    // 変数まとめ
    #region Field
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
    float defaultDrop = 0.5f; // デフォルトの値(データが読み込まない場合)
    float dropTime = 0.5f;      // 時間設定
    float dropTimerCnt = 0.0f;  // カウント用

    // 移動制限
    int Cy = 9;
    int Cx = 3;

    // 次のコマの生成タイミング
    bool canNext = true;
    // 負け判定
    bool gameLoseFlg = false;

    // 削除処理
    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;

    // ネクスト表示
    NextPuzzleUI next;
    [SerializeField] Image[] images;
    #endregion

    // GameManagerで呼び出し
    public void Game()
    {
        P2Input();
        ChainPuzzle();
        Droppuzzle();
    }
    /// <summary>
    /// 落下速度の設定
    /// </summary>
    public float SetUp
    {
        set { dropTime = value; }
    }
    /// <summary>
    /// 入力処理
    /// </summary>
    private void P2Input()
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
            dropTime = 0.1f;  // ドロップ速度を速くする
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            dropTime = defaultDrop;  // 通常の落下速度に戻す
        }
    }

    /// <summary>
    /// 盤面にコマを生成する
    /// </summary>
    public void GeneObj()
    {
        // 連鎖中の場合は生成しない
        if (puzzleCleared) { return; }
        NextOBJ();
        timer = 0;
        Cy = 9;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y + 0.7f);
        ParentObj = Instantiate(puzzleQueue.Dequeue(), initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P2.transform;
        mypuzzleObj.Add(ParentObj);
        canNext = false;
    }

    // ネクスト表示関連メソッド
    #region NEXTOBJ
    /// <summary>
    /// インスタンス生成メソッド
    /// </summary>
    private void Instance()
    {
        // インスタンス生成
        next = new NextPuzzleUI();
    }
    // 最初の表示
    private void SetNextObj()
    {
        // リストに格納
        List<GameObject> listObj = new List<GameObject>(puzzleQueue);
        images[0].sprite = listObj[0].GetComponent<SpriteRenderer>().sprite;
        images[1].sprite = listObj[1].GetComponent<SpriteRenderer>().sprite;
    }

    public void STARTSET()
    {
        Instance();
        SetNextObj();
    }

    private void NextOBJ()
    {
        next.NextObjUI(puzzleQueue, images);
    }
    #endregion

    // 負け判定関連メソッド
    #region LOSE
    /// <summary>
    /// 負け判定
    /// </summary>
    private void LoseMethod()
    {
        for (int i = 0; i < mypuzzleObj.Count; i++)
        {
            // 真ん中の一番上に置かれたら負け
            if (mypuzzleObj[i].transform.position.y >= listRIGHT[9].posColumns[3].y &&
                mypuzzleObj[i].transform.position.x == listRIGHT[9].posColumns[3].x)
            {
                gameLoseFlg = true;
            }
        }
    }

    public bool IsLose
    {
        get { return gameLoseFlg; }
    }
    #endregion

    // コマの移動に関するメソッド（盤面も含む）
    #region PUZZLEMOVE
    // 落下処理
    private void Droppuzzle()
    {
        if (canNext) { return; }
        dropTimerCnt += Time.deltaTime;

        if (dropTimerCnt >= dropTime)
        {
            dropTimerCnt = 0;
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                canNext = true;
            }
            else
            {
                if (SetPuzzle(ParentObj))
                {
                    ParentObj.transform.position = new Vector2(listRIGHT[Cy].posColumns[Cx].x, listRIGHT[Cy].posColumns[Cx].y);
                    Cy--;
                }
                else
                {
                    CheckAndClearPuzzle();
                    LoseMethod();
                    canNext = true;
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

        float targetX = listRIGHT[Cy].posColumns[Cx - 1].x;
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
        if (Cx >= listRIGHT[Cy].posColumns.Length - 1) return false;

        float targetX = listRIGHT[Cy].posColumns[Cx + 1].x;
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
    public bool CanNextPuzzle { get { return canNext; } }

    /// <summary>
    /// 盤面の座標を貰う
    /// </summary>
    public void RecieveList(List<RIGHTWolrdVec2> list)
    {
        listRIGHT = list;
    }
    #endregion

    // 揃ったコマの削除と連鎖チェック
    #region PUZZLEDELETE
    /// <summary>
    /// 駒が消えるべきかチェックし、消す処理
    /// </summary>
    private void CheckAndClearPuzzle()
    {
        // 横方向のチェック
        for (int y = 0; y < listRIGHT.Count; y++)
        {
            for (int x = 0; x < listRIGHT[y].posColumns.Length; x++)
            {
                if (ClearPuzzleIfSurrounded(x, y, 1, 0)) // 横方向のチェック (dx=1, dy=0)
                {
                    puzzleCleared = true;
                }
            }
        }

        // 縦方向のチェック
        for (int x = 0; x < listRIGHT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listRIGHT.Count; y++)
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
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 1:
                    CheckAndClearPuzzle();
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num++;
                    }
                    break;
                case 2:
                    ShiftPuzzlesDown();
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        puzzleCleared = false;
                        CheckAndClearPuzzle();
                        timer = 0;
                        num++;
                    }
                    break;
                case 3:
                    timer += Time.deltaTime;
                    if (timer >= 0.3f)
                    {
                        timer = 0;
                        num = 0;
                    }
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
        // 始点の駒を取得
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        // 終点の駒を取得
        int oppositeX = x + dx * 2;
        int oppositeY = y + dy * 2;
        GameObject endPuzzle = GetPuzzleAt(oppositeX, oppositeY);

        if (endPuzzle == null) return false;

        // スプライトを取得
        Sprite startSprite = GetPuzzleSprite(startPuzzle);
        Sprite endSprite = GetPuzzleSprite(endPuzzle);

        // 始点と終点のスプライトが一致している場合
        if (startSprite == endSprite && startSprite != null)
        {
            // 中間の駒を取得
            GameObject middlePuzzle = GetPuzzleAt(x + dx, y + dy);
            if (middlePuzzle != null)
            {
                Sprite middleSprite = GetPuzzleSprite(middlePuzzle);

                // 中間のスプライトが異なる場合
                if (middleSprite != startSprite)
                {
                    RemovePuzzle(startPuzzle);
                    RemovePuzzle(middlePuzzle);
                    RemovePuzzle(endPuzzle);
                    return true; // 駒を消した場合はtrueを返す
                }
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
    private Sprite GetPuzzleSprite(GameObject puzzle)
    {
        SpriteRenderer spriteRenderer = puzzle.GetComponent<SpriteRenderer>();
        return spriteRenderer != null ? spriteRenderer.sprite : null;
    }

    /// <summary>
    /// コマを下にシフトさせる
    /// </summary>
    private void ShiftPuzzlesDown()
    {
        for (int x = 0; x < listRIGHT[0].posColumns.Length; x++)
        {
            for (int y = 0; y < listRIGHT.Count; y++)
            {
                Vector2 currentPos = listRIGHT[y].posColumns[x];
                GameObject currentPuzzle = GetPuzzleAtPosition(currentPos);

                if (currentPuzzle == null)
                {
                    for (int aboveY = y + 1; aboveY < listRIGHT.Count; aboveY++)
                    {
                        Vector2 abovePos = listRIGHT[aboveY].posColumns[x];
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
        if (y < 0 || y >= listRIGHT.Count || x < 0 || x >= listRIGHT[y].posColumns.Length)
            return null;

        Vector2 targetPos = listRIGHT[y].posColumns[x];
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
    #endregion
}