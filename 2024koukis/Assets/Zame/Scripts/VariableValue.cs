using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableValue : MonoBehaviour
{
    // レベルデザインする際の
    // 変数をデータをもとに設定
    public void SETUP()
    {
        SetDropTimeFromVariables();
    }

    #region DROP
    // 落下速度
    float defaultDrop = 0.5f;
    float dropSpd;

    // jsonデータから変数を設定するためのメソッド
    private void SetDropTimeFromVariables()
    {
        // 例: 変数名が "defaultDrop" で値が float 型の場合
        var dropTimeVariable = VariableManager.Instance.Variables.Find(v => v.Name == "defaultDrop");
        if (dropTimeVariable != null && float.TryParse(dropTimeVariable.Value, out float parsedDropTime))
        {
            // データを基にデフォルトの値変更
            defaultDrop = parsedDropTime;
            Debug.Log("defaultDrop が設定されました: " + defaultDrop);
        }
        else // 存在しない場合は既存の速度に設定
        {
            if (dropTimeVariable == null)
            {
                Debug.LogWarning("defaultDrop 変数が JSON ファイルに存在しません。デフォルト値を使用します。");
            }
            else
            {
                Debug.LogWarning($"defaultDrop の値 '{dropTimeVariable.Value}' は無効です。デフォルト値を使用します。");
            }
            // データがない場合は既存の速度に
            dropSpd = defaultDrop;
        }
        // プレイヤーに渡すためセット
        dropSpd = defaultDrop;
    }

    public float DROPSPD
    {
        get { return dropSpd; }
    }
    #endregion
}
