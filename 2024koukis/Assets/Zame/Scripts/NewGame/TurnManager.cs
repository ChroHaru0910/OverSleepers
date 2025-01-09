using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Field
    // �v���C���[
    public enum Turn
    {
        Player1,
        Player2,
        Members,
    }

    // ��{�I��Player1����U
    // �����������Ōォ�猈�߂Ȃ���
    Turn turn=Turn.Player1;

    // PL1,2�̏��ԂŃI�u�W�F�N�g��ݒ�
    [Header("�A���[�I�u�W�F�N�g������"), SerializeField] GameObject[] arrowObj;

    // ��������
    private System.Random random = new System.Random();

    // �����ۑ�
    int randNum = 0;
    #endregion

    /// <summary>
    /// �N�̃^�[�����Ԃ��܂�
    /// </summary>
    public Turn GetTurn
    {
        // �l��Ԃ��Ɠ����ɖ��I�u�W�F�N�g����������
        get {
            // ��x�S�Ĕ�\��
            FalseArrow();
            // �s���^�[���̐l�̖��I�u�W�F�N�g��\������
            arrowObj[(int)turn].SetActive(true);
            return turn;
        }
    }

    /// <summary>
    /// �^�[���ύX
    /// </summary>
    public void TurnChange()
    {
        turn = turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;
    }

    /// <summary>
    /// �S�Ĕ�\���ɂ���
    /// </summary>
    private void FalseArrow()
    {
        for(int i=0;i<(int)Turn.Members;i++)
        {
            arrowObj[i].SetActive(false);
        }
    }

    /// <summary>
    /// ��U�����߂鏈��
    /// </summary>
    public void TurnSelect()
    {
        // ���������߂Ă��̒l�Ő������߂�
        randNum=random.Next(0, (int)Turn.Members);  // ������܂߂�ꍇ
        switch (randNum)
        {
            case 0:
                turn = Turn.Player1;
                break;
            case 1:
                turn = Turn.Player2;
                break;
        }
    }
}
