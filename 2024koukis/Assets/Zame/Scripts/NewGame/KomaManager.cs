using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaManager : MonoBehaviour
{
    // 変数まとめ
    #region Field
    // 生成した駒をまとめるオブジェクト
    [SerializeField] GameObject P;
    // 生成するコマの情報
    [SerializeField] GameObject[] puzzleObj;
    // 座標リスト
    List<WolrdVec2> listLEFT = new List<WolrdVec2>();
    // 盤面のコマオブジェクト保存リスト
    List<GameObject> mypuzzleObj = new List<GameObject>();
    
    // 生成時の座標を計算
    private Vector2 Pv2;
    // 操作中のコマ
    GameObject ParentObj;

    // ドロップさせる
    bool isDrop = false;

    // 移動制限
    int Cy = 5;
    int Cx = 3;

    // 次のコマの生成タイミング
    bool canNext = true;

    // 削除処理
    bool puzzleCleared = false;
    int num = 0;
    float timer = 0;

    // 削除予定のオブジェクトをまとめる
    List<GameObject> trash = new List<GameObject>();
    bool isCheck = false;
    #endregion

    // GameManagerで呼び出し
    public void Game()
    {
        PlInput();
        ChainPuzzle();
        Droppuzzle();
    }

    /// <summary>
    /// 入力処理
    /// </summary>
    private void PlInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Cx == 0) { return; }
            Cx--;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Cx == 5) { return; }
            Cx++;
            ParentObj.transform.position = new Vector2(listLEFT[Cy].posColumns[Cx].x, ParentObj.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isDrop = true;  // ドロップさせる
        }
    }

    /// <summary>
    /// 盤面にコマを生成する
    /// </summary>
    public void GeneObj(TurnManager.Turn turn)
    {
        // 連鎖中の場合は生成しない
        if (puzzleCleared) { return; }

        // 初期状態に戻す
        isCheck = false;

        Cy = 5;
        Cx = 3;
        Vector2 initialPosition = new Vector2(listLEFT[Cy].posColumns[Cx].x, listLEFT[Cy].posColumns[Cx].y);
        ParentObj = Instantiate(puzzleObj[(int)turn], initialPosition, Quaternion.identity);
        ParentObj.transform.parent = P.transform;
        mypuzzleObj.Add(ParentObj);
        canNext = false;
    }

    // コマの移動に関するメソッド（盤面も含む）
    #region PUZZLEMOVE
    // 落下処理
    private void Droppuzzle()
    {
        if (canNext) { return; }

        if (isDrop)
        {
            if (Cy < 0)
            {
                CheckAndClearPuzzle();
                canNext = true;
                isDrop = false;
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
                    canNext = true;
                    isDrop = false;
                }
            }
        }
        return;
    }

    // 現在操作中のコマの下に別のコマが存在するか
    private bool SetPuzzle(GameObject obj)
    {
        float targetY = obj.transform.position.y - 1.5f;

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
    /// 次のコマを生成するタイミングを返す
    /// </summary>
    public bool CanNextPuzzle { get { return canNext; } }
    public bool IsChained { get { return puzzleCleared; } }
    /// <summary>
    /// 盤面の座標を貰う
    /// </summary>
    public void RecieveList(List<WolrdVec2> list)
    {
        listLEFT = list;
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

        isCheck = true;
    }

    /// <summary>
    /// 指定された位置の駒を削除すべきか判定し、削除する処理
    /// </summary>
    private bool ClearPuzzleIfSurrounded(float x, float y, float dx, float dy)
    {
        // 始点の駒取得
        GameObject startPuzzle = GetPuzzleAt(x, y);
        if (startPuzzle == null) return false;

        Sprite startSprite = GetPuzzleSprite(startPuzzle);
        if (startSprite == null) return false;

        // 中間駒リスト
        List<GameObject> middlePuzzles = new List<GameObject>();
        List<Sprite> middleSprites = new List<Sprite>();
        GameObject endPuzzle = null;

        // 中間駒の探索
        int maxSearchRange = 10;
        for (int i = 1; i < maxSearchRange; i++)
        {
            float checkX = x + dx * i;
            float checkY = y + dy * i;

            GameObject currentPuzzle = GetPuzzleAt(checkX, checkY);
            if (currentPuzzle == null) break;

            Sprite currentSprite = GetPuzzleSprite(currentPuzzle);
            if (currentSprite == null) break;

            if (currentSprite == startSprite)
            {
                endPuzzle = currentPuzzle; // 終点を設定
                break;
            }

            middlePuzzles.Add(currentPuzzle);
            middleSprites.Add(currentSprite);
        }

        if (endPuzzle == null || middlePuzzles.Count == 0) return false;

        // 中間駒の色チェック
        Sprite firstMiddleSprite = middleSprites[0];
        foreach (Sprite sprite in middleSprites)
        {
            if (sprite != firstMiddleSprite) return false;
        }

        if (firstMiddleSprite == startSprite) return false;

        // 駒を削除
        foreach (GameObject puzzle in middlePuzzles) RemovePuzzle(puzzle);
        RemovePuzzle(startPuzzle);
        RemovePuzzle(endPuzzle);

        return true;
    }

    /// <summary>
    /// 指定した駒を削除する
    /// </summary>
    private void RemovePuzzle(GameObject puzzle)
    {
        if (mypuzzleObj.Contains(puzzle))
        {
            //mypuzzleObj.Remove(puzzle);
            trash.Add(puzzle);
            //Destroy(puzzle);
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
    /// 指定位置のパズルオブジェクトを取得
    /// </summary>
    private GameObject GetPuzzleAt(float x, float y)
    {
        if (y < 0 || y >= listLEFT.Count || x < 0 || x >= listLEFT[(int)y].posColumns.Length)
            return null;

        Vector2 targetPos = listLEFT[(int)y].posColumns[(int)x];
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
    #endregion

    #region CHAINPUZZLE
    /// <summary>
    /// 連鎖処理
    /// </summary>
    private void ChainPuzzle()
    {
        if (puzzleCleared)
        {
            switch (num)
            {
                case 0:
                    if (isCheck)
                    {
                        foreach (GameObject obj in trash)
                        {
                            mypuzzleObj.Remove(obj);
                            Destroy(obj);
                        }
                        isCheck = false;
                    }
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

                case 1:
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
    /// 駒を下にシフトさせる
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
