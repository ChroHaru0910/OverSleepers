using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEFT_PuzzleBoard : MonoBehaviour
{
    // �e�I�u�W�F�N�g
    [SerializeField] GameObject ParentObj;

    // �|�W�V���������X�g�ɕۑ�
    List<LEFTWolrdVec2> objList = new List<LEFTWolrdVec2>();

    //�q�v�f�N���X���g�p���Ĕz����쐬
    public ChildArray[] rows;
    [System.Serializable]
    public class ChildArray
    {
        public GameObject[] columns;
    }

    // ���W�ϊ���ۑ�
    LEFTWolrdVec2[] vec2Rows;    
   
    #region METHOD
    // �z��̏�����
    void Initialize()
    {
        // `vec2Rows` �� `rows.Length` �ɍ��킹�ď�����
        vec2Rows = new LEFTWolrdVec2[rows.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            // �e `WolrdVec2` �C���X�^���X��������
            vec2Rows[i] = new LEFTWolrdVec2();
            vec2Rows[i].posColumns = new Vector2[rows[i].columns.Length];
        }
    }

    // ���X�g�ǉ�
    void ListSet()
    {
        for(int i=0;i<vec2Rows.Length;i++)
        {
            objList.Add(vec2Rows[i]);
        }
    }

    // ���[���h���W�ɕϊ�
    void WldPosition()
    {
        for(int i=0;i<rows.Length;i++)
        {
            for(int j=0;j<rows[i].columns.Length;j++)
            {
                vec2Rows[i].posColumns[j]= ParentObj.transform.TransformPoint(rows[i].columns[j].transform.localPosition);
            }
        }
    }

    #endregion

    // �}�l�[�W���[.cs�ɂĎ��s����
    public void LeftSet()
    {
        Initialize();
        WldPosition();
        ListSet();
        Destroy(ParentObj);
    }

    // ���X�g�̃f�[�^��n��
    // �Q�[���}�l�[�W���[.cs��
    public List<LEFTWolrdVec2> ReturnList()
    {
        return objList;
    }

    // Debug.Log( p.ReturnList()[0].posColumns[0]);
}
public class LEFTWolrdVec2
{
    public Vector2[] posColumns;
}
