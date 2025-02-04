using System.IO;
using UnityEngine;
public class SaveInt : MonoBehaviour
{
    // �ۑ�����f�[�^
    public int[] data;

    // �v���W�F�N�g���̕ۑ��p�X
    private string saveFilePath = "Assets/Zame/Settings/IntArray.json";

    public void SaveData()
    {
        try
        {
            // �z��f�[�^�����b�v���� JSON �ɕϊ�
            IntArrayData saveData = new IntArrayData { values = data };
            string jsonData = JsonUtility.ToJson(saveData, true); // �C���f���g�t�� JSON

            // �w�肳�ꂽ�p�X�ɕۑ�
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
                // JSON �t�@�C������ǂݍ���
                string jsonData = File.ReadAllText(saveFilePath);
                IntArrayData loadedData = JsonUtility.FromJson<IntArrayData>(jsonData);
                data = loadedData.values;

                Debug.Log("Data loaded successfully.");
            }
            else
            {
                Debug.LogWarning($"Save file not found at: {saveFilePath}");
                InitializeData(5); // �f�t�H���g�l��ݒ�
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load data: {ex.Message}");
        }
    }

    private void InitializeData(int size)
    {
        // �z���������
        data = new int[size];
    }

    private void PrintData()
    {
        // �f�[�^���e���R���\�[���ɕ\��
        Debug.Log($"Data: {string.Join(", ", data)}");
    }

    // �f�[�^�̕ۑ��E�ǂݍ��݂��e�X�g�ł���f�o�b�O�p�{�^��
    [ContextMenu("Save Data")]
    public void Save() => SaveData();

    [ContextMenu("Load Data")]
    public void Load() => LoadData();

    [ContextMenu("Print Data")]
    public void Print() => PrintData();

    // �f�[�^�\��
    [System.Serializable]
    private class IntArrayData
    {
        public int[] values;
    }

    /// <summary>
    /// �������̌v�Z�������̂𑗂荇��
    /// </summary>
    public int[] GetInt
    {
        set { data = value; }
        get { return data; }
    }
}
