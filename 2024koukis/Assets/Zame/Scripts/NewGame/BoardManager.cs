using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // 親オブジェクト
    [SerializeField] GameObject ParentObj;

    // ポジションをリストに保存
    List<WolrdVec2> objList = new List<WolrdVec2>();

    //子要素クラスを使用して配列を作成
    public ChildArray[] rows;
    [System.Serializable]
    public class ChildArray
    {
        public GameObject[] columns;
    }

    // 座標変換を保存
    WolrdVec2[] vec2Rows;


    #region METHOD
    // 配列の初期化
    void Initialize()
    {
        // `vec2Rows` を `rows.Length` に合わせて初期化
        vec2Rows = new WolrdVec2[rows.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            // 各 `WolrdVec2` インスタンスを初期化
            vec2Rows[i] = new WolrdVec2();
            vec2Rows[i].posColumns = new Vector2[rows[i].columns.Length];
        }
    }

    // リスト追加
    void ListSet()
    {
        for (int i = 0; i < vec2Rows.Length; i++)
        {
            objList.Add(vec2Rows[i]);
        }
    }

    // ワールド座標に変換
    void WldPosition()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].columns.Length; j++)
            {
                vec2Rows[i].posColumns[j] = ParentObj.transform.TransformPoint(rows[i].columns[j].transform.localPosition);
            }
        }
    }

    #endregion

    // マネージャー.csにて実行する
    public void BoardSet()
    {
        Initialize();
        WldPosition();
        ListSet();
        Destroy(ParentObj);
    }

    // リストのデータを渡す
    // ゲームマネージャー.csに
    public List<WolrdVec2> ReturnList()
    {
        return objList;
    }

}
public class WolrdVec2
{
    public Vector2[] posColumns;
}

