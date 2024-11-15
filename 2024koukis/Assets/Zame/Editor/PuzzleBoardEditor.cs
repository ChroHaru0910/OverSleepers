using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PuzzleBoardEditor : EditorWindow
{
    private int rows = 5;
    private int columns = 5;
    private GameObject boardParent;

    private float puzzleSize = 0.7f; 

    [MenuItem("Tools/Puzzle Board Editor")]
    public static void ShowWindow()
    {
        GetWindow<PuzzleBoardEditor>("Puzzle Board Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Puzzle Board Settings", EditorStyles.boldLabel);

        rows = EditorGUILayout.IntField("Rows", rows);
        columns = EditorGUILayout.IntField("Columns", columns);
        puzzleSize = EditorGUILayout.FloatField("size",puzzleSize);

        if (GUILayout.Button("Generate Board"))
        {
            GenerateBoard();
        }
    }

    private void GenerateBoard()
    { 
        //// �����̃{�[�h���폜
        //if (boardParent != null)
        //{
        //    DestroyImmediate(boardParent);
        //}

        // �{�[�h�S�̂̐e�I�u�W�F�N�g���쐬
        boardParent = new GameObject("PuzzleBoard");

        // �e�s�𐶐�
        for (int y = 0; y < rows; y++)
        {
            GameObject rowParent = new GameObject($"Row_{y}");
            rowParent.transform.parent = boardParent.transform;

            for (int x = 0; x < columns; x++)
            {
                GameObject cell = new GameObject($"Cell_{x}_{y}");

                // ���[�J�����W�ňʒu��ݒ�
                cell.transform.localPosition = new Vector2(puzzleSize * x, puzzleSize * y);
                cell.transform.parent = rowParent.transform;

                Vector3 worldPosition = boardParent.transform.TransformPoint(cell.transform.localPosition);
            }
        }
    }
}
