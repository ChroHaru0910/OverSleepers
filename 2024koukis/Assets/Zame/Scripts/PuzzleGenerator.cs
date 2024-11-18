using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    // �g�p����p�Y���̋�
    [Header("�p�Y���I�u�W�F�N�g"), SerializeField] GameObject[] pObj;
    // �p�Y���̋��ۑ����Ă���
    private Queue<GameObject> puzzleQueue=new Queue<GameObject>();

    // ��������
    private System.Random random = new System.Random();

    // �v���C���[�X�N���v�g
    [SerializeField] P1Controller p1;
    [SerializeField] P2Controller p2;

    // �L���[�̒l��ۑ�����
    private GameObject saveObj;
    /// <summary>
    /// �p�Y���̋���L���[�ɂ����
    /// </summary>
    private void QueueSet()
    {
        // ��芸�����L���[�ɂ�100�ǉ�
        if (puzzleQueue.Count <= 100)
        {
            puzzleQueue.Enqueue(pObj[GetRandomNumber(0, pObj.Length)]);
        }
    }

    // �����擾
    private int GetRandomNumber(int min, int max)
    {
        return random.Next(min, max);  // ������܂߂�ꍇ
    }

    // �p�Y���̋��n��
    private GameObject SendQueue()
    {
        return puzzleQueue.Dequeue();
    }

    // �v���C���[�X�N���v�g�ɃI�u�W�F�N�g�𑗂荞��
    private void SendObj()
    {
        // �L���[�̐擪������o��
        saveObj = SendQueue();
        p1.SetObj = saveObj; // p1
        p2.SetObj = saveObj; // p2
    }


    /// <summary>
    /// �p�Y���������ăv���C���[�����ɑ���
    /// </summary>
    public void GENERATOR()
    {
        QueueSet();
        SendObj();
    }
}
