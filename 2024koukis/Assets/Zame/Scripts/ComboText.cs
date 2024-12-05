using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    // �R���{���𔽉f������e�L�X�g
    [SerializeField] Text[] comboText;

    public enum Player
    {
        P1,
        P2
    }

    public void OutCombo(int chain,Player pl)
    {
        // �A���Ȃ����͕����\�����Ȃ�
        if (chain <= 0)
        {
            comboText[(int)pl].text ="";
            return;
        }
        comboText[(int)pl].text = chain.ToString() + " Combo";
    }
}
