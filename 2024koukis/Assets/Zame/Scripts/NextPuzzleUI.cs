using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NextPuzzleUI
{
    /// <summary>
    /// ���̃R�}��\������
    /// </summary>
    /// <param name="nextObj">�v���C���[���ꂼ�ꎝ���Ă��鐶����������</param>
    /// <param name="img">�\����</param>
   public void NextObjUI(Queue<GameObject> nextObj,Image[] img)
    {
        // ���X�g�Ɋi�[
        List<GameObject> listObj = new List<GameObject>(nextObj);

        // ���
        GameObject nextobj = listObj[1];
        GameObject secNextobj= listObj[2];
        img[0].sprite = nextobj.GetComponent<SpriteRenderer>().sprite;
        img[1].sprite = secNextobj.GetComponent<SpriteRenderer>().sprite;
    }
}
