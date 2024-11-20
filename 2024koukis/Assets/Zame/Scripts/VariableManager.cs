using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class VariableInfo
{
    public string Name;
    public string Type;
    public string Value;
    public string Comment;
}

public class VariableManager : MonoBehaviour
{
    public static VariableManager Instance { get; private set; }

    // JSON ファイルのパス
    string jsonFilePath = "C:/Users/user/Desktop/study/team/2024kouki/LevelManager/LevelManager/bin/Debug/Variables.json";
    string jsonFilePathCopy = "Assets/Zame/Settings/Level.json";
    string jsonFilePathUNITY = "Zame/Settings/Level.json";
    public List<VariableInfo> Variables { get; private set; } = new List<VariableInfo>();

    private void Awake()
    {
        // Singleton の実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されない
        }
        else
        {
            Debug.LogWarning("VariableManager の複数インスタンスが作成されました。既存のインスタンスを使用します。");
            Destroy(gameObject); // 重複したインスタンスを破棄
        }
    }

    public void LoadJson()
    {
        CopyJsonFile(jsonFilePath, jsonFilePathCopy);
        LoadVariables();
        DisplayVariables();
    }

    private void LoadVariables()
    {
        string fullPath = Path.Combine(Application.dataPath, jsonFilePathUNITY);

        // ファイルが存在する場合はデータを読み込む
        try
        {
            string json = File.ReadAllText(fullPath);
            Variables = JsonUtility.FromJson<VariableListWrapper>(WrapJson(json)).Variables;
            Debug.Log("変数が正常にロードされました。");
        }
        catch (Exception ex)
        {
            Debug.LogError($"JSONのロード中にエラーが発生しました: {ex.Message}");
        }
    }

    /// <summary>
    /// JSONファイルをコピーする
    /// </summary>
    /// <param name="sourcePath">コピー元ファイルのパス</param>
    /// <param name="destinationPath">コピー先ファイルのパス</param>
    private void CopyJsonFile(string sourcePath, string destinationPath)
    {
        if (File.Exists(sourcePath))
        {
            try
            {
                // 元のJSONファイルの内容を読み込む
                string jsonData = File.ReadAllText(sourcePath);

                // コピー先のパスにデータを書き込む
                File.WriteAllText(destinationPath, jsonData);

                Debug.Log("JSONファイルが正常にコピーされました！");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("JSONファイルのコピーに失敗しました: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("コピー元のJSONファイルが見つかりません: " + sourcePath);
        }
    }
private void DisplayVariables()
    {
        foreach (var variable in Variables)
        {
            Debug.Log($"Name: {variable.Name}, Type: {variable.Type}, Value: {variable.Value}, Comment: {variable.Comment}");
        }
    }

    // ここでJSONをラップする処理
    private string WrapJson(string json)
    {
        return "{\"Variables\":" + json + "}";
    }

    [Serializable]
    private class VariableListWrapper
    {
        public List<VariableInfo> Variables;
    }
}