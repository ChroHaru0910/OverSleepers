using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NextPuzzleUI
{
    /// <summary>
    /// 次のコマを表示する
    /// </summary>
    /// <param name="nextObj">プレイヤーそれぞれ持っている生成情報を入れる</param>
    /// <param name="img">表示先</param>
   public void NextObjUI(Queue<GameObject> nextObj,Image[] img)
    {
        // リストに格納
        List<GameObject> listObj = new List<GameObject>(nextObj);

        // 一個目
        GameObject nextobj = listObj[1];
        GameObject secNextobj= listObj[2];
        img[0].sprite = nextobj.GetComponent<SpriteRenderer>().sprite;
        img[1].sprite = secNextobj.GetComponent<SpriteRenderer>().sprite;
    }
}
