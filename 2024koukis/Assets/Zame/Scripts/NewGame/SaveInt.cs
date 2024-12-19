using System.IO;
using UnityEngine;
public class SaveInt : MonoBehaviour
{
    // 保存するデータ
    public int[] data;

    // プロジェクト内の保存パス
    private string saveFilePath = "Assets/Zame/Settings/IntArray.json";

    public void SaveData()
    {
        try
        {
            // 配列データをラップして JSON に変換
            IntArrayData saveData = new IntArrayData { values = data };
            string jsonData = JsonUtility.ToJson(saveData, true); // インデント付き JSON

            // 指定されたパスに保存
            File.WriteAllText(saveFilePath, jsonData);
            Debug.Log($"Data saved to: {saveFilePath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save data: {ex.Message}");
        }
    }

    public void LoadData()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                // JSON ファイルから読み込み
                string jsonData = File.ReadAllText(saveFilePath);
                IntArrayData loadedData = JsonUtility.FromJson<IntArrayData>(jsonData);
                data = loadedData.values;

                Debug.Log("Data loaded successfully.");
            }
            else
            {
                Debug.LogWarning($"Save file not found at: {saveFilePath}");
                InitializeData(5); // デフォルト値を設定
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load data: {ex.Message}");
        }
    }

    private void InitializeData(int size)
    {
        // 配列を初期化
        data = new int[size];
    }

    private void PrintData()
    {
        // データ内容をコンソールに表示
        Debug.Log($"Data: {string.Join(", ", data)}");
    }

    // データの保存・読み込みをテストできるデバッグ用ボタン
    [ContextMenu("Save Data")]
    public void Save() => SaveData();

    [ContextMenu("Load Data")]
    public void Load() => LoadData();

    [ContextMenu("Print Data")]
    public void Print() => PrintData();

    // データ構造
    [System.Serializable]
    private class IntArrayData
    {
        public int[] values;
    }

    /// <summary>
    /// 勝利数の計算したものを送り合う
    /// </summary>
    public int[] GetInt
    {
        set { data = value; }
        get { return data; }
    }
}
