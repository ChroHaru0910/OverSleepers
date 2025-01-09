using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStar : MonoBehaviour
{
    #region Field
    // �e�L�X�g�ɂĉ����E���h���������\������
    [Header("win����\������e�L�X�g"), SerializeField] Text[] win;

    // ���݂��̃v���C���[�̏�����
    int[] num = new int[2];

    // �e�L�X�g�}�l�[�W���[�ɏ��҂�n��
    [SerializeField] TextManager textMng;

    [SerializeField] SaveInt intData;
    #endregion

    #region Function
    /// <summary>
    /// �Q�[���I�����̏���
    /// </summary>
    /// <param name="turn">���҂̖��O</param>
    public void Win(TurnManager.Turn turn)
    {
        // �ۑ������f�[�^��ǂݍ���
        num = intData.GetInt;
        // �����v���C���[�̖��O���e�L�X�g�ɔ��f
        textMng.WinText(turn.ToString());
        // ���������̂Ő��}�[�N���l��
        num[(int)turn]++;
        // �ǉ����Ă���ۑ�����
        intData.GetInt = num;
        intData.SaveData();
        // �v���C���[�̐��}�[�N�̐��l�𔽉f
        win[(int)turn].text =num[(int)turn].ToString();
    }

    // GameManager�ɂčŏ��ɓǂݍ���
    /// <summary>
    /// �������}�[�N�̔��f����
    /// </summary>
    public void SetWin()
    {
        intData.LoadData();
        num = intData.GetInt;
        for(int i=0;i<num.Length;i++)
        {
            win[i].text = num[i].ToString();
        }
    }

    /// <summary>
    /// �t�@�[�X�g���E���h�Ɏ��s��
    /// �������̓��E���h�I����Đ킷��O�Ɏ��s
    /// </summary>
    public void WinsReset()
    {
        Debug.Log("�f�[�^�����Z�b�g���܂��B");

        for(int i=0;i<(int)TurnManager.Turn.Members;i++)
        {
            num[i] = 0;
        }
        // �ۑ�����
        intData.GetInt = num;
        intData.SaveData();
    }
    #endregion
}
