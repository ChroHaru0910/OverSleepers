using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    #region Field
    // ���s��\������
    [Header("�������v���C���[��\������"), SerializeField] Text resultText;
    // �Q�[���̏������v���C���[�ɒm�点��V�X�e���e�L�X�g
    [Header("�V�X�e���e�L�X�g"),SerializeField] Text systemText;
    #endregion

    #region Function
    /// <summary>
    /// �e�L�X�g�Z�b�g
    /// </summary>
    /// <param name="text">�\������������</param>
    public void SystemSetText(string text)
    {
        systemText.text = text;
    }
    /// <summary>
    /// �\�����Ă���V�X�e���e�L�X�g����������
    /// </summary>
    /// <param name="reset">�����Ȃ�true��n����</param>
    public void SystemSetText(bool reset)
    {
        if (reset) { systemText.text = ""; }
    }

    /// <summary>
    /// �������v���C���[��\������
    /// </summary>
    /// <param name="name">���҃v���C���[�̖��O��n��</param>
    public void WinText(string name)
    {
        resultText.text = name + "WIN";
    }

    public void Draw()
    {
        resultText.text = "DRAW";
    }
    #endregion
}
