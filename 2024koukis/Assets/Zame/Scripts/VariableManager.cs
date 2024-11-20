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

    // JSON �t�@�C���̃p�X
    string jsonFilePath = "C:/Users/user/Desktop/study/team/2024kouki/LevelManager/LevelManager/bin/Debug/Variables.json";
    string jsonFilePathCopy = "Assets/Zame/Settings/Level.json";
    string jsonFilePathUNITY = "Zame/Settings/Level.json";
    public List<VariableInfo> Variables { get; private set; } = new List<VariableInfo>();

    private void Awake()
    {
        // Singleton �̎���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��j������Ȃ�
        }
        else
        {
            Debug.LogWarning("VariableManager �̕����C���X�^���X���쐬����܂����B�����̃C���X�^���X���g�p���܂��B");
            Destroy(gameObject); // �d�������C���X�^���X��j��
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

        // �t�@�C�������݂���ꍇ�̓f�[�^��ǂݍ���
        try
        {
            string json = File.ReadAllText(fullPath);
            Variables = JsonUtility.FromJson<VariableListWrapper>(WrapJson(json)).Variables;
            Debug.Log("�ϐ�������Ƀ��[�h����܂����B");
        }
        catch (Exception ex)
        {
            Debug.LogError($"JSON�̃��[�h���ɃG���[���������܂���: {ex.Message}");
        }
    }

    /// <summary>
    /// JSON�t�@�C�����R�s�[����
    /// </summary>
    /// <param name="sourcePath">�R�s�[���t�@�C���̃p�X</param>
    /// <param name="destinationPath">�R�s�[��t�@�C���̃p�X</param>
    private void CopyJsonFile(string sourcePath, string destinationPath)
    {
        if (File.Exists(sourcePath))
        {
            try
            {
                // ����JSON�t�@�C���̓��e��ǂݍ���
                string jsonData = File.ReadAllText(sourcePath);

                // �R�s�[��̃p�X�Ƀf�[�^����������
                File.WriteAllText(destinationPath, jsonData);

                Debug.Log("JSON�t�@�C��������ɃR�s�[����܂����I");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("JSON�t�@�C���̃R�s�[�Ɏ��s���܂���: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("�R�s�[����JSON�t�@�C����������܂���: " + sourcePath);
        }
    }
private void DisplayVariables()
    {
        foreach (var variable in Variables)
        {
            Debug.Log($"Name: {variable.Name}, Type: {variable.Type}, Value: {variable.Value}, Comment: {variable.Comment}");
        }
    }

    // ������JSON�����b�v���鏈��
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