using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableValue : MonoBehaviour
{
    // ���x���f�U�C������ۂ�
    // �ϐ����f�[�^�����Ƃɐݒ�
    public void SETUP()
    {
        SetDropTimeFromVariables();
    }

    #region DROP
    // �������x
    float defaultDrop = 0.5f;
    float dropSpd;

    // json�f�[�^����ϐ���ݒ肷�邽�߂̃��\�b�h
    private void SetDropTimeFromVariables()
    {
        // ��: �ϐ����� "defaultDrop" �Œl�� float �^�̏ꍇ
        var dropTimeVariable = VariableManager.Instance.Variables.Find(v => v.Name == "defaultDrop");
        if (dropTimeVariable != null && float.TryParse(dropTimeVariable.Value, out float parsedDropTime))
        {
            // �f�[�^����Ƀf�t�H���g�̒l�ύX
            defaultDrop = parsedDropTime;
            Debug.Log("defaultDrop ���ݒ肳��܂���: " + defaultDrop);
        }
        else // ���݂��Ȃ��ꍇ�͊����̑��x�ɐݒ�
        {
            if (dropTimeVariable == null)
            {
                Debug.LogWarning("defaultDrop �ϐ��� JSON �t�@�C���ɑ��݂��܂���B�f�t�H���g�l���g�p���܂��B");
            }
            else
            {
                Debug.LogWarning($"defaultDrop �̒l '{dropTimeVariable.Value}' �͖����ł��B�f�t�H���g�l���g�p���܂��B");
            }
            // �f�[�^���Ȃ��ꍇ�͊����̑��x��
            dropSpd = defaultDrop;
        }
        // �v���C���[�ɓn�����߃Z�b�g
        dropSpd = defaultDrop;
    }

    public float DROPSPD
    {
        get { return dropSpd; }
    }
    #endregion
}
