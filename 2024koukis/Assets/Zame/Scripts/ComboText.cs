using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    // コンボ数を反映させるテキスト
    [SerializeField] Text[] comboText;

    public enum Player
    {
        P1,
        P2
    }

    public void OutCombo(int chain,Player pl)
    {
        // 連鎖ない時は文字表示もなし
        if (chain <= 0)
        {
            comboText[(int)pl].text ="";
            return;
        }
        comboText[(int)pl].text = chain.ToString() + " Combo";
    }
}
